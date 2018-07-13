using Candidate.Core;
using Candidate.Dialog;
using Candidate.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Candidate.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private DateTimeOffset LastSync = DateTimeOffset.MinValue;
        //private ObservableCollection<Profile> profiles = ProfileData.Instance.Profiles;

        public event PropertyChangedEventHandler PropertyChanged;

        private string updateInfoText;
        public string UpdateInfoText
        {
            get { return updateInfoText; }
            set
            {
                updateInfoText = value;
                OnPropertyChanged("UpdateInfoText");
            }
        }

        private string progressDialogText;
        public string ProgressDialogText
        {
            get { return progressDialogText; }
            set
            {
                progressDialogText = value;
                OnPropertyChanged("ProgressDialogText");
            }
        }



        public HomeViewModel()
        {          
            DateTime savedDate = CrossSettings.Current.GetValueOrDefault("LastSync", DateTime.UtcNow);
            UpdateInfoText = "Última atualização em " + savedDate.ToShortDateString() + ", "
                        + savedDate.ToShortTimeString();
        }

        public async void TryUpdate(bool forceUpdate)
        {
            ContentView v = new ProgressDialog();
            (v as ProgressDialog).BindingContext = this;
            PopupPage p = new ProgressDialogPage(v);

            ProgressDialogText = "Refreshing data";

            //was already open
            if (forceUpdate || !CrossSettings.Current.GetValueOrDefault("AppFirstOpen", false))            
            {
                CrossSettings.Current.AddOrUpdateValue("AppFirstOpen", true);
            


            await PopupNavigation.PushAsync(p);

            //ObservableCollection<ProfileQuestion> profiles = ProfileData.Instance.Profiles;
            DateTime dt = CrossSettings.Current.GetValueOrDefault("LastSync", DateTime.UtcNow);

            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    bool inSync = await Sync();
                    //save lastSync
                    if (inSync)
                    {
                        ProgressDialogText = "Successful";                            
                        CrossSettings.Current.AddOrUpdateValue("LastSync", DateTime.UtcNow);                           
                    }
                    else
                    {
                            ProgressDialogText = "Fail. Try again later";
                    }
                }
                else
                {
                        ProgressDialogText = "Fail. There is no Internet connection";
                }
            }
            finally
            {
                    //DateTime savedDate = CrossSettings.Current.GetValueOrDefault("LastSync", DateTime.UtcNow);
                    //UpdateInfoText = "Última atualização em " + savedDate.ToShortDateString()+", "
                    //    + savedDate.ToShortTimeString();
                await Task.Delay(1500);
                await PopupNavigation.PopAsync();
            }
        }


        }


        public async Task<bool> Sync()
        {
            //var changed = profiles.Where(p => p.LastUpdate >= LastSync || (p.Deleted != null && p.Deleted >= LastSync));

            bool ret = false;
            string url = ConstantHolder.AllProfileQuestions;
            List<ProfileQuestion> profileQuestions = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        ret = true;

                        try
                        {
                            profileQuestions = await Task.Run(() => 
                            JsonConvert.DeserializeObject<List<ProfileQuestion>>(json));
                        }
                        catch (Exception e)
                        {
                            ret = false;
                        }


                        if (profileQuestions != null)
                        {                            
                            //drop and recreate database
                            ProfileData.Instance.CreateDatabase();
                         
                            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DatabaseLocation))
                            {
                                conn.InsertOrReplaceAllWithChildren(profileQuestions, recursive: true);
                            }

                            ////fresh data to memory
                            await Task.Factory.StartNew(() => ProfileData.Instance.SqliteToMemory());
                        }
                    }
                }
            }
            catch(Exception e)
            {
                e.GetBaseException();
            }

                return ret;
        }

        void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}

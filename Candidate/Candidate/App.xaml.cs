using Candidate.Models;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Candidate
{
	public partial class App : Application
	{
        public static string DatabaseLocation = string.Empty;
        

		public App ()
		{
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());
            DatabaseLocation = databaseLocation;
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            if(!CrossSettings.Current.Contains("LastSync"))
                CrossSettings.Current.AddOrUpdateValue("LastSync", DateTime.UtcNow);
            CrossSettings.Current.AddOrUpdateValue("AppFirstOpen", false);
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

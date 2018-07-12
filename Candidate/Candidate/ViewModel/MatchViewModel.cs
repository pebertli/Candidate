using Candidate.Core;
using Candidate.Dialog;
using Candidate.Models;
using Candidate.ViewModel.Command;
using Candidate.ViewModel.Commands;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Syncfusion.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Candidate.ViewModel
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        private int position;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        //public ProfileData ProfileDataInstance;
        //public ObservableCollection<Question> Questions { get; set; }
        //public ObservableCollection<Profile> Profiles { get; set; }



        public ICommand TapCommand { get; private set; }
        public ProfileQuestionTapCommand ProfileQuestionCommand { get; private set; }
        public InfoCommand InfoCommand { get; private set; }
        public LeftCommand LeftCommand { get; private set; }
        public RightCommand RightCommand { get; private set; }

        private string infoTextPopup;
        public string InfoTextPopup
        {
            get { return infoTextPopup; }
            set
            {
                infoTextPopup = value;
                OnPropertyChanged("InfoTextPopup");
            }
        }

        private string infoProfileQuestionPopup;
        public string InfoProfileQuestionPopup
        {
            get { return infoProfileQuestionPopup; }
            set
            {
                infoProfileQuestionPopup = value;
                OnPropertyChanged("InfoProfileQuestionPopup");
            }
        }

        private Assertive selectedItem;
        public Assertive SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                //selectedItem = null;
                OnPropertyChanged("SelectedItem");
            }
        }

        private bool leftEnabled;
        public bool LeftEnabled
        {
            get { return leftEnabled; }
            set
            {
                leftEnabled = value;
                OnPropertyChanged("LeftEnabled");
            }
        }

        private bool rightEnabled;
        public bool RightEnabled
        {
            get { return rightEnabled; }
            set
            {
                rightEnabled = value;
                OnPropertyChanged("RightEnabled");
            }
        }

        private bool scoreVisible;

        public bool ScoreVisible
        {
            get { return scoreVisible; }
            set
            {
                scoreVisible = value;
                OnPropertyChanged("ScoreVisible");
            }
        }


        private KNNPrediction knn;


        public MatchViewModel()
        {
            //ProfileDataInstance = ProfileData.Instance;
            //Questions = ProfileData.Instance.Questions;
            //Profiles = ProfileData.Instance.Profiles;

            //AssertiveListView = assertiveListView;
            TapCommand = new Command<Assertive>(AssertiveTap);
            ProfileQuestionCommand = new ProfileQuestionTapCommand(this);
            //InfoButtonClickedCommand = new Command<object>(InfoButtonClicked);       
            InfoCommand = new InfoCommand(this);
            LeftCommand = new LeftCommand(this);
            RightCommand = new RightCommand(this);

            LeftEnabled = false;
            RightEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public void AssertiveTap(Assertive a)
        {
            Question q = ProfileData.Instance.Questions.ElementAt(Position);
            foreach(Assertive assertive in q.Assertives)
            {                
                    assertive.IsChecked = (a == assertive);             
            }

            UpdateScores();
            ScoreVisible = true;
            

            
        }

        private void UpdateScores()
        {
           //questions ordened by Id that needs to be the same order of Features
           var qs = ProfileData.Instance.Questions.OrderBy(x => x.Id);
           float[] test = new float[ProfileData.Instance.Questions.Count()];
            //get score value of each question based on the choiced assertive
           foreach(Question q in qs)
           {
                Assertive a = q.Assertives.Where(x => x.IsChecked == true).SingleOrDefault();
                //as Id starts from 1
                test[q.Id-1] = a != null ? a.Score : -1;                
           }

            float[] scores = knn.ClassifyRanked(test, 1);
            var ps = ProfileData.Instance.Profiles.OrderBy(x => x.Id);
            foreach(Profile p in ps)
            {
                //as Id starts from 1
                p.Score = ScoreToPercentage(scores[p.Id-1]);
                //p.Score = scores[p.Id];
            }

        }

        public async void InfoTap(object parameter)
        {
            ContentView v = new InfoDialog();
            PopupPage p = new InfoDialogPage(v);
            InfoTextPopup = (string) parameter;
            await PopupNavigation.PushAsync(p);
        }

        public async void ProfileQuestionTap(Profile profile)
        {
            //List<ProfileQuestion> lpq = ProfileQuestion.GetAllWithChildren();
            var op = ProfileData.Instance.ProfileQuestions.SingleOrDefault(pq => pq.Profile.Id == profile.Id
            && pq.Question.Id == ProfileData.Instance.Questions[Position].Id);
            ContentView v = new InfoDialog();
            PopupPage p = new InfoDialogPage(v);            
            InfoTextPopup = op.Opinion;

            await PopupNavigation.PushAsync(p);
        }

        public void LeftTap(object parameter)
        {
            if (Position > 0)
            {
                if (--Position == 0)
                    LeftEnabled = false;
                else
                    LeftEnabled = true;                                
            }

            if (Position < ProfileData.Instance.Questions.Count() - 1)                           
                    RightEnabled = true;                
        }

        public void RightTap(object parameter)
        {
            if (Position < ProfileData.Instance.Questions.Count() - 1)
            {
                if (++Position == ProfileData.Instance.Questions.Count()-1)
                    RightEnabled = false;
                else
                    RightEnabled = true;
            }

            if (Position > 0)                            
                    LeftEnabled = true;

            

        }

        public void OnAppearing()
        {
            float[][] trainData = ProfileData.Instance.GetTrainData();
            knn = new KNNPrediction(ProfileData.Instance.Questions.Count, ProfileData.Instance.Profiles.Count
                , trainData, 0, 4);
            //Questions.CollectionChanged
            //Questions = ProfileData.Instance.Questions;
            //Profiles = ProfileData.Instance.Profiles;



            //double[] test = { 4, 4 };
            //int k = 1;
            ////int predicted = knn.ClassifySingle(test, k);
            //float[] rank = knn.ClassifyRanked(test, k);

            //float minValue = 0; //Assertive A
            //float maxValue = 4; //Assertive E
            //double MaxDistance = Math.Sqrt(ProfileData.Instance.Questions.Count * (maxValue - minValue)* (maxValue - minValue));

            //rank[0] = (float)((Math.Abs(rank[0] - MaxDistance) * 100) / MaxDistance);
            //rank[1] = (float) ((Math.Abs(rank[1] - MaxDistance) * 100) / MaxDistance);
            //rank[2] = (float) ((Math.Abs(rank[2] - MaxDistance) * 100) / MaxDistance);

            //App.Current.MainPage.DisplayAlert("Resultado", (rank[0]).ToString()+" "+rank[1].ToString()
            //    +" "+rank[2].ToString(), "OK");
        }

        private float ScoreToPercentage(float value)
        {
            double MaxDistance = Math.Sqrt(ProfileData.Instance.Questions.Count * knn.MaxDistance);
            return (float) ((Math.Abs(value - MaxDistance) * 100) / MaxDistance);
        }
    }
}

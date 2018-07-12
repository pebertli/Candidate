using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Candidate.Models
{
    public class ProfileQuestion : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public float Choice { get; set; }
        
        private Profile profile;
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Profile Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged("Profile");
            }
        }

        [ForeignKey(typeof(Profile))]
        public int ProfileId { get; set; }

        private Question question;
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Question Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        [ForeignKey(typeof(Question))]
        public int QuestionId { get; set; }

        private string opinion;
        public string Opinion
        {
            get { return opinion; }
            set
            {
                opinion = value;
                OnPropertyChanged("Opinion");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public static List<ProfileQuestion> GetAllWithChildren()
        {
            List<ProfileQuestion> ret = new List<ProfileQuestion>();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                ret = connection.GetAllWithChildren<ProfileQuestion>(recursive: true);
            }

            return ret;
        }

        //public static ProfileQuestion GetProfileQuestion(long ProfileId, long questionId )
        //{           
        //    return GetAllWithChildren().SingleOrDefault(pq => pq.Profile.Id == ProfileId && pq.Question.Id == questionId);
                        
        //}

        public static int Count()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                return connection.Table<ProfileQuestion>().Count();
            }
        }

    }  
}

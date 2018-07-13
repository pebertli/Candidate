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
    public class Question :INotifyPropertyChanged
    {
        [PrimaryKey, Unique, AutoIncrement]        
        public int Id { get; set; }

        //[ForeignKey(typeof(ProfileQuestion))]
        //public int ProfileQuestionId { get; set; }      

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }

        
        private List<Assertive> assertives;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Assertive> Assertives
        {
            get { return assertives; }
            set
            {
                assertives = value;
                OnPropertyChanged("Assertives");
            }
        }     

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public static List<Question> GetAllWithChildren()
        {
            List<Question> ret = new List<Question>();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                ret = connection.GetAllWithChildren<Question>(recursive: true);
            }

            return ret;
        }

        public static int Count()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                return connection.Table<Question>().Count();
            }
        }
    }

}

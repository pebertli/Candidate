using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Candidate.Models
{   
    public class Assertive : INotifyPropertyChanged
    {
        [PrimaryKey, Unique, AutoIncrement]       
        public int Id { get; set; }

        [ForeignKey(typeof(Question))]
        public int QuestionId { get; set; }

        //Inverse relationships, these are optional
        //[ManyToOne("QuestionId", "Assertives")]
        //public Question Question { get; set; }

        public int Score { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }       
    }
}

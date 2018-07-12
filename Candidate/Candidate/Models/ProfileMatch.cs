using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Candidate.Models
{
    class ProfileMatch : INotifyPropertyChanged
    {

        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged("Profile");
            }
        }

        private float match;

        public float Match
        {
            get { return match; }
            set
            {
                match = value;
                OnPropertyChanged("Match");
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
    }
}

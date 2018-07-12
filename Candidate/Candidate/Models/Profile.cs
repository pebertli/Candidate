using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Models 
{
    public class Profile : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]        
        public int Id { get; set; }

        //[ForeignKey(typeof(ProfileQuestion))]
        //public int ProfileQuestionId { get; set; }

        ////Inverse relationships, these are optional
        //[OneToMany("ProfileQuestionId", "Profile")]
        ////[ManyToOne]
        //public ProfileQuestion ProfileQuestion { get; set; }

        //public DateTimeOffset LastUpdate { get; set; }

        //public DateTimeOffset? Deleted { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string bio;
        public string Bio
        {
            get { return bio; }
            set
            {
                bio = value;
                OnPropertyChanged("Bio");
            }
        }

        private string imageFile;
        public string ImageFile
        {
            get { return imageFile; }
            set
            {
                imageFile = value;
                OnPropertyChanged("ImageFile");
            }
        }

        private float score;
        public float Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        public Profile()
        {
            Score = 0;
            ImageFile = "bolsonaro_round.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public static List<Profile> GetAll()
        {
            List<Profile> ret = new List<Profile>();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                ret = connection.Table<Profile>().ToList();
            }

            return ret;

        }

        public static int Count()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                return connection.Table<Profile>().Count();
            }
        }

    }
}
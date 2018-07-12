using Candidate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Candidate.ViewModel
{
   

    public class ProfileViewModel
    {        
        public ObservableCollection<Profile> Profiles { get; set; }

        public ProfileViewModel()
        {

            Profiles = ProfileData.Instance.Profiles;
        }
    }
}

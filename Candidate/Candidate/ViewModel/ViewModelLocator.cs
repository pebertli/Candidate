using System;
using System.Collections.Generic;
using System.Text;

namespace Candidate.ViewModel
{
    public static class ViewModelLocator
    {
        private static MatchViewModel _MatchViewModel = new MatchViewModel();
        public static MatchViewModel MainViewModel
        {
            get
            {
                return _MatchViewModel;
            }
        }

    }
}

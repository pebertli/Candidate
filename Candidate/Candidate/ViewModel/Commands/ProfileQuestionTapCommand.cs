using Candidate.Models;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Candidate.ViewModel.Commands
{
    public class ProfileQuestionTapCommand : ICommand
    {
        public MatchViewModel vm { get; set; }

        public ProfileQuestionTapCommand(MatchViewModel vm)
        {
            this.vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ItemTappedEventArgs arg = (ItemTappedEventArgs) parameter;
            Profile profile = (Profile) arg.ItemData;
            vm.ProfileQuestionTap(profile);
        }
    }
}

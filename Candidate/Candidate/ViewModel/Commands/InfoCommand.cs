using Candidate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Candidate.ViewModel.Command
{
    public class InfoCommand : ICommand
    {
        public MatchViewModel vm { get; set; }

        public InfoCommand(MatchViewModel vm)
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
            string p = (string) parameter;
            vm.InfoTap(p);
        }
    }
}

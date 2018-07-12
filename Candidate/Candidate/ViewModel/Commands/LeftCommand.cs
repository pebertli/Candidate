using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Candidate.ViewModel.Commands
{
    public class LeftCommand :ICommand
    {
        public MatchViewModel vm { get; set; }

        public LeftCommand(MatchViewModel vm)
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
            int p = (int) parameter;
            vm.LeftTap(p);
        }
    }
}
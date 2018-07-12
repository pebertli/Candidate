using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Candidate.ViewModel.Commands
{
    public class RightCommand :ICommand
    {
        public MatchViewModel vm { get; set; }

        public RightCommand(MatchViewModel vm)
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
            vm.RightTap(p);
        }
    }
}
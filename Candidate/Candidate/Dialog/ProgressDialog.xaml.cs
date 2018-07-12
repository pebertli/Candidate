using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Candidate.Dialog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressDialog : ContentView
	{
        //public Label LabelInfo { get; set; }
        public ProgressDialog ()
		{
			InitializeComponent ();            

            //LabelInfo = this.LabelInfoProgress;
		}
	}
}
using Candidate.ViewModel;
using Rg.Plugins.Popup.Services;
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
	public partial class InfoDialog : ContentView
	{
        public InfoDialog ()
		{
			InitializeComponent ();
            BindingContext = ViewModelLocator.MainViewModel;
            //vm = Application.Current.FindResource("MyViewModel") as MatchViewModel;

        }

        void CloseButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

    }
}
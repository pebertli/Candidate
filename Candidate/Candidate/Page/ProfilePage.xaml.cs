using Candidate.Models;
using Candidate.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Candidate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
      


        //ProfileViewModel vm;

		public ProfilePage ()
		{
			InitializeComponent ();

            BindingContext = new ProfileViewModel();

            

            
		}
	}
}
using Candidate.Models;
using Candidate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Candidate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchPage : ContentPage
	{

        private MatchViewModel vm;

		public MatchPage ()
		{       
            InitializeComponent ();

            this.Title = "Match";

            vm = ViewModelLocator.MainViewModel;
            //ListView lv = NameScopeExtensions.FindByName<ListView>(this, );
            BindingContext = vm;

            //vm = new MatchViewModel(/*this.FindByName<ListView>("AssertiveListView")*/);            
            //BindingContext = vm;    
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearing();
        }        

    }
}
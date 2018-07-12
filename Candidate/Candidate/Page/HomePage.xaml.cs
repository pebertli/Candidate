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
    public partial class HomePage : ContentPage
	{

        HomeViewModel vm;

		public HomePage()
		{
			InitializeComponent();

            this.Title = "Encontre seu candidato";

            vm = new HomeViewModel();
            BindingContext = vm;

            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += (object sender, EventArgs e) => {
                vm.TryUpdate(true);
            };
            tapRecognizer.NumberOfTapsRequired = 1;
            TapImage.GestureRecognizers.Add(tapRecognizer);



        }

        private async void ProfileButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void MatchButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MatchPage());
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
          
            vm.TryUpdate(false);

            
        }

        private void UpdateInfoClicked(object sender, EventArgs e)
        {
            vm.TryUpdate(true);
        }
    }
}

using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Candidate.Dialog
{
    class ProgressDialogPage : PopupPage
    {        

        public ProgressDialogPage(Xamarin.Forms.View contentBody)
        {
            Content = contentBody;
            //this.HasSystemPadding = false;             
            this.BackgroundColor = new Color(0, 0, 0, 0.4);
            this.CloseWhenBackgroundIsClicked = true;
            //PageClosedTaskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<T>();


        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        //protected override Task OnAppearingAnimationEnd()
        //{
        //    return Content.FadeTo(1);
        //}

        //// Method for animation child in PopupPage
        //// Invoked before custom animation begin
        //protected override Task OnDisappearingAnimationBegin()
        //{
        //    return Content.FadeTo(1);
        //}

        protected override bool OnBackButtonPressed()
        {
            // Prevent back button pressed action on android
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Prevent background clicked action
            //return base.OnBackgroundClicked();
            return false;
        }

    }
}

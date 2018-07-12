using System;
using Candidate.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Candidate")]
[assembly: ExportEffect(typeof(EffectTransparentBackground), "EffectTransparentBackground")]
namespace Candidate.Droid
{

    public class EffectTransparentBackground : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Control != null)
                    this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                else
                {                    
                    Container.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", (object) ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
    }
}
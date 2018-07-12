using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Candidate.View
{
    public class DotButton : BoxView
    {
        public int index;
        public DotButtonsLayout layout;
        public event ClickHandler Clicked;
        public delegate void ClickHandler(DotButton sender);
        public DotButton()
        {
            var clickCheck = new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (Clicked != null)
                    {
                        Clicked(this);
                    }
                })
            };
            GestureRecognizers.Add(clickCheck);
        }
    }

}

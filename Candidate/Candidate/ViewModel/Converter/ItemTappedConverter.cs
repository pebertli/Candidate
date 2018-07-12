using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Candidate.ViewModel.Converter
{
    class ItemTappedConverter :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)

        {

            ItemTappedEventArgs eventArgs = value as ItemTappedEventArgs;                        

            return eventArgs.Item;

        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {

            throw new NotImplementedException();

        }

    }
}

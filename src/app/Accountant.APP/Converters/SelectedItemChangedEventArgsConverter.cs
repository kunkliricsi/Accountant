using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Accountant.APP.Converters
{
    class SelectedItemChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SelectedItemChangedEventArgs args)
                return args.SelectedItem;

            throw new ArgumentException($"Expected '{nameof(SelectedItemChangedEventArgs)}' but got '{value.GetType()}'.", "value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

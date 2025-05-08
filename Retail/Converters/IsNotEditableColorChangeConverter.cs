using System;
using System.Globalization;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class IsNotEditableColorChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            if ((Boolean)value == true)
                return Color.White;
            else
                return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

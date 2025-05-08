using System;
using System.Globalization;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class LabelColorChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            if ((Boolean) value == true)
                return Color.Red;
            else
                return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

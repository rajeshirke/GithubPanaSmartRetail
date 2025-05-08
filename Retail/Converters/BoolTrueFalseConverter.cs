using System;
using System.Globalization;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class BoolTrueFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (System.Convert.ToInt16(value) == 0)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

using System;
using System.Globalization;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class ExpanderIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return ImageSource.FromResource("expandarrow.png");
            else
                return ImageSource.FromResource("downarrowgray.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

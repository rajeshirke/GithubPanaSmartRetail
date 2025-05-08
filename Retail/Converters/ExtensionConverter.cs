using System;
using System.Globalization;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class ExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            if ((value.ToString().ToLower().Contains(".pdf")) && (parameter.ToString() == "pdf"))
            {
                return true;
            }
            else if (((value.ToString().ToLower().Contains(".mp4")) || (value.ToString().ToLower().Contains(".mov"))) && (parameter.ToString() == "video"))
            {
                return true;
            }
            else if ((parameter.ToString() == "image") && ((value.ToString().ToLower().Contains(".jpeg")) || (value.ToString().ToLower().Contains(".jpg")) || (value.ToString().ToLower().Contains(".png")) || (value.ToString().ToLower().Contains(".gif")) || (value.ToString().ToLower().Contains(".bmp"))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

using System;
using System.Globalization;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class TaskSummaryTickConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            var value1 = value as int?;

            if (value1 > 0)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

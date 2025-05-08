using System;
using System.Globalization;
using Retail.Infrastructure.RequestModels;
using Xamarin.Forms;

namespace Retail.Converters
{
    public class LabelValueChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var value1 = value as SalesEntryRequest;

            if (value1.SelectedReturn)
                return value1.TotalPrice = -(value1.TotalPrice);
            else
                return value1.TotalPrice = (value1.TotalPrice);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

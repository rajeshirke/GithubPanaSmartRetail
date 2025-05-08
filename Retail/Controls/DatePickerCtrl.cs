using System;
using Xamarin.Forms;

namespace Retail.Controls
{
    public class DatePickerCtrl : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder",
            returnType: typeof(string), declaringType: typeof(DatePickerCtrl),
            defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceholderColor), typeof(string), typeof(DatePickerCtrl), "#C1C1C1", BindingMode.TwoWay);

        public string PlaceholderColor
        {
            get { return (string)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(DatePickerCtrl), true);

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public static BindableProperty SelectedDateProperty =
           BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(TitledDateTimePicker), null, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Retail.Controls
{
    public partial class TitledDateTimePicker : ContentView
    {
        public TitledDateTimePicker()
        {
            InitializeComponent();
            BindingContext = this;

        }
        public static BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(TitledDateTimePicker), null, BindingMode.TwoWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // SelectedDate = Convert.ToDateTime(newValue) ;
            //var control = bindable as DatePickerEntryCell;
            //if (control != null)
            //{
            //    // do something with this control...
            //}
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            propertyName: nameof(SDate),
            returnType: typeof(DateTime),
            declaringType: typeof(TitledDateTimePicker),
            defaultValue: new DateTime(1990, 01, 01),
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime SDate
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
            propertyName: nameof(MinDate),
            returnType: typeof(DateTime),
            declaringType: typeof(TitledDateTimePicker),
            defaultValue: new DateTime(1990, 01, 01),
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime MinDate
        {
            get => (DateTime)GetValue(MinDateProperty);
            set => SetValue(MinDateProperty, value);
        }

        public static BindableProperty SelectedDateProperty =
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(TitledDateTimePicker), null, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {

            Device.InvokeOnMainThreadAsync(() => {
                if (dPicker.IsFocused)
                    dPicker.Unfocus();

                dPicker.Focus();
            });

        }

        void dPicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            SDate = e.NewDate;
            Command?.Execute(null);
            ItemTapped(sender, e);
        }
        public static readonly BindableProperty CommandProperty =
              BindableProperty.Create("Command", typeof(ICommand), typeof(TitledDateTimePicker), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(TitledDateTimePicker), null);

        public event EventHandler ItemTapped = (e, a) => { };



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

    }
}

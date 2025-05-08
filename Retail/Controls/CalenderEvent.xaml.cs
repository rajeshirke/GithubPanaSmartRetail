using System;
using System.Collections.Generic;
using System.Windows.Input;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalenderEvent : ContentView
    {
        public static BindableProperty CalenderEventCommandProperty =
           BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalenderEvent), null);

        public CalenderEvent()
        {
            InitializeComponent();
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (BindingContext is PersonDailyAttendanceResponse eventModel)
                CalenderEventCommand?.Execute(eventModel);
        }
    }
}

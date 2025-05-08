using System;
using System.Collections.Generic;
using Retail.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        internal DayView()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (BindingContext is DayModel dayModel && !dayModel.IsDisabled && dayModel.IsVisible)
            {
                dayModel.IsSelected = true;
                dayModel.DayTappedCommand?.Execute(dayModel.Date);
            }
        }
    }
}

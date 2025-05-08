using System;
using System.Collections.Generic;
using Retail.ViewModels.Dashboard;
using Xamarin.Forms;

namespace Retail.Views.Dashboard
{
    public partial class NotificationPage : ContentPage
    {
        NotificationsViewModel viewModel { get; set; }
        public NotificationPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NotificationsViewModel(Navigation);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.Attendance;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PromoterAttendance : ContentPage
    {
        PromoterAttendanceViewModel viewModel { get; set; }
        public PromoterAttendance()
        {
            InitializeComponent();
            BindingContext = viewModel = new PromoterAttendanceViewModel(Navigation);
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.GetLocation();
                IsBusy = false;
            });
        }

        async void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            await viewModel.IsDayOffCheckedChange();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}

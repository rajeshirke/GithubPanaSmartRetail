using System;
using System.Collections.Generic;
using Retail.DependencyServices;
using Retail.ViewModels.MonthYearPickerViewModel;
using Retail.ViewModels.SalesTarget;
using Xamarin.Forms;

namespace Retail.Views.SalesTargetViews
{
    public partial class SalesDataReturnEntry : ContentPage
    {
        SalesDataReturnEntryViewModel viewModel { get; set; }
        public SalesDataReturnEntry()
        {            
            InitializeComponent();            
            BindingContext = viewModel = new SalesDataReturnEntryViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, bool>(this, "AllowSubmit", (obj, _IsEnableSubmit) => {
                viewModel.IsEnableSaveSalesEntry = _IsEnableSubmit;
            });
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }

        void TitledDateTimePicker_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.TotalCount = "0";
            viewModel.SelectedDate = dp.Date;
            
        }
    }
}

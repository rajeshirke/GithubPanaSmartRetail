using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.SalesTarget;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Retail.Views.SalesTargetViews
{
    public partial class SalesDataEntry : ContentPage
    {
        
        SalesDataEntryViewModel viewModel { get; set; }
        public SalesDataEntry()
        {
            InitializeComponent();
            BindingContext =viewModel= new SalesDataEntryViewModel(Navigation);
            viewModel.GetLocation();
            viewModel.SelectedDateVisible = true;
            //selectDate.DateSelected += DateDatePicker_DateSelected;
            selectDate.Unfocused += SelectDate_Unfocused;
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
           
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            var mindate = DateTime.Now.AddDays(-7);
            selectDate.MinimumDate = mindate;
        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            Expander expander = label.Parent.Parent.Parent as Expander;

            if (label.FontSize == Device.GetNamedSize(NamedSize.Default, label))
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
            }
            else
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Default, label);
            }
            expander.ForceUpdateSize();
        }

        private void SelectDate_Unfocused(object sender, FocusEventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.TotalCount = "0";
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            MessagingCenter.Subscribe<object, string>(this, "SelectedModelNo", (obj, _ModelNo) => {
                txtModelNo.Text = _ModelNo;
            });

            MessagingCenter.Subscribe<object, bool>(this, "AllowSubmit", (obj, _IsEnableSubmit) => {
                viewModel.IsEnableSaveSalesEntry = _IsEnableSubmit;
            });
        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
                    
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        void TitledDateTimePicker_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.TotalCount = "0";            
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
        }

        async void txtModelNo_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            await viewModel.ValidateModelNumber();
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.InvokeOnMainThreadAsync(() => {
                if (selectDate.IsFocused)
                    selectDate.Unfocus();

                selectDate.Focus();
            });
        }
        
    }
}

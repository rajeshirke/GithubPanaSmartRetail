using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Retail.ViewModels.PriceTracker;

namespace Retail.Views.PriceTracker
{
    public partial class StoreSelectionPopupPriceTracker
    {
        public PriceTrackerViewModel viewModel { get; set; }
        public DisplayTrackerViewModel DisplayTrackerViewModel { get; set; }
        public int DisplayFlag { get; set; }

        public StoreSelectionPopupPriceTracker()
        {
            InitializeComponent();
            BindingContext = viewModel = new PriceTrackerViewModel(Navigation);
        }

        public StoreSelectionPopupPriceTracker(int _DisplayFlag)
        {
            InitializeComponent();
            DisplayFlag = _DisplayFlag;
            BindingContext = DisplayTrackerViewModel = new DisplayTrackerViewModel(Navigation);
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async void search_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (DisplayFlag == 1)
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    DisplayTrackerViewModel.SearchStoreCommand.Execute(e.NewTextValue);
                }
                else
                {
                    await DisplayTrackerViewModel.GetLocation();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    viewModel.SearchStoreCommand.Execute(e.NewTextValue);
                }
                else
                {
                    await viewModel.GetLocation();
                }
            }            
        }
    }
}


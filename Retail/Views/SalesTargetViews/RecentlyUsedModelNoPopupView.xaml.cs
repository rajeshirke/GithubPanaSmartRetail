using System;
using System.Collections.Generic;
using Retail.ViewModels.SalesTarget;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.SalesTargetViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentlyUsedModelNoPopupView
    {
        ProductCategoryPopupViewModel viewModel { get; set; }
        public RecentlyUsedModelNoPopupView(int flag)
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductCategoryPopupViewModel(Navigation,flag);
            viewModel.GetRecentlyUsedModelNos();
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Retail.Controls;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.SalesTarget;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Views.SalesTargetViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCategoryPopupView 
    {
        ProductCategoryPopupViewModel viewModel { get; set; }
        public int MasterCategoryId { get; set; }
   
        public ProductCategoryPopupView(int flag)
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductCategoryPopupViewModel(Navigation, flag);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.GetProductbyCategory();
            });                       
        }       

        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }      

    }
}

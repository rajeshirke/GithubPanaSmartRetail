using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.ViewModels.InventoryStock;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.Views.InventoryStock
{
    public partial class ConfirmInventoryPopup 
    {
        ConfirmInventoryPopupVewModel viewModel { get; set; }
        public ConfirmInventoryPopup(List<InventoryStockEntryRequest> lstInventoryStockEntryRequest) 
        {
            InitializeComponent();
            BindingContext = viewModel = new ConfirmInventoryPopupVewModel(Navigation, lstInventoryStockEntryRequest);
            InventoryLists.ItemsSource = lstInventoryStockEntryRequest;

            if(lstInventoryStockEntryRequest.FirstOrDefault().StockEntryTypeId== (int)StockEntryTypeEnum.OutOfStockEntry)
            {
                txtSaveNotifyme.Text = "Notify";
            }
            else
            {
                txtSaveNotifyme.Text = "Save";
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            bool IsSubmitEnable = true;
            MessagingCenter.Send<object, bool>(this, "AllowSubmitInventory", IsSubmitEnable);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

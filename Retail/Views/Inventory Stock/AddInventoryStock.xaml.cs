    using System;
using System.Collections.Generic;
using System.Linq;
using Retail.Database;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.InventoryStock;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Retail.Views.InventoryStock
{
    public partial class AddInventoryStock : ContentPage
    {
        ADdInventoryViewModel viewModel { get; set; }
        public InventoryDataEntryDb inventoryDataEntryDb;
        public AddInventoryStock()
        {
            InitializeComponent();
            BindingContext = viewModel = new ADdInventoryViewModel(Navigation);
            viewModel.GetLocation();            
            inventoryDataEntryDb = new InventoryDataEntryDb();
            rbOutofStock.IsVisible = false;
            rbInventoryStock.IsVisible = false;

            var _mindate = DateTime.Now.AddDays(-4);
            dpInventory.MinDate = _mindate;

            if (CommonAttribute.CustomeProfile!=null)
            {                
                List<MobileMainMenuResponse> DBMobileAppModules = (List<MobileMainMenuResponse>)CommonAttribute.CustomeProfile.MobileMainMenus;
                if (DBMobileAppModules != null)
                {
                    var DBMobileAppModulesAsc = DBMobileAppModules.OrderBy(p => p.MenuOrder).ToList();

                    foreach (var MobileAppModule in DBMobileAppModulesAsc)
                    {
                        if(MobileAppModule.Name.ToLower().Contains("inventory stock"))
                        {
                            root.Title = "Inventory Stock Entry";
                            rbInventoryStock.IsVisible = true;
                            rbInventoryStock.IsChecked = true;
                        }
                        if (MobileAppModule.Name.ToLower().Contains("out of stock entry"))
                        {
                            root.Title = "Inventory Stock Entry";
                            rbOutofStock.IsVisible = true;
                            rbOutofStock.IsChecked = true;
                        }
                    }
                }
            }

        }   
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, bool>(this, "AllowSubmitInventory", (obj, _IsEnableSubmit) => {
                viewModel.IsEnableInventoryEntry = _IsEnableSubmit;
            });

            MessagingCenter.Subscribe<object, string>(this, "SelectedModelNo", (obj, _ModelNo) => {
                txtModelNo.Text = _ModelNo;
            });
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

        void rbInventoryStock_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if(rbInventoryStock.IsChecked)
            {
                txtQty.IsVisible = true;
                lblQty.IsVisible = true;
                viewModel.IsQtyListVisible = true;
                viewModel.InventoryFlag =(int) StockEntryTypeEnum.InventoryStockEntry;
                viewModel.EntryDateEnable = true;

                if (viewModel.obInventoryStockEntryRequest != null && viewModel.obInventoryStockEntryRequest.Count != 0)
                {
                    viewModel.obInventoryStockEntryRequest = new System.Collections.ObjectModel.
                        ObservableCollection<Infrastructure.RequestModels.InventoryStockEntryRequest>();                    
                }

                viewModel.GetInventoryEntryList();
            }            
        }

        void rbOutofStock_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (rbOutofStock.IsChecked)
            {
                txtQty.IsVisible = false;
                lblQty.IsVisible = false;
                viewModel.IsQtyListVisible = false;
                viewModel.InventoryFlag = (int)StockEntryTypeEnum.OutOfStockEntry;
                viewModel.Quantity = 0;
                viewModel.EntryDateEnable = false;

                if (viewModel.obInventoryStockEntryRequest != null && viewModel.obInventoryStockEntryRequest.Count != 0)
                {                    
                    viewModel.obInventoryStockEntryRequest = new System.Collections.ObjectModel.
                        ObservableCollection<Infrastructure.RequestModels.InventoryStockEntryRequest>();
                }
                viewModel.GetInventoryEntryList();
            }          
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
    }
}

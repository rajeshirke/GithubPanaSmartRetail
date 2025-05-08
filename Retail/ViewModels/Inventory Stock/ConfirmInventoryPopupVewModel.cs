using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.InventoryStock
{
    public class ConfirmInventoryPopupVewModel : BaseViewModel
    {
        public InventoryDataEntryDb inventoryDataEntryDb;
        public Connection c;
        public TblInventoryStockEntryRequest tblInventoryStockEntryRequest;
        public ConfirmInventoryPopupVewModel(INavigation navigation, List<InventoryStockEntryRequest> lstInventoryStockEntryRequest) : base(navigation)
        {
            IsSaveInventoryEnable = true;
            lstSaveInventoryStockEntryRequest = new List<InventoryStockEntryRequest>();
            lstSaveInventoryStockEntryRequest = lstInventoryStockEntryRequest;
            TotalCount = lstInventoryStockEntryRequest.Count.ToString();
            inventoryDataEntryDb = new InventoryDataEntryDb();
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public Command SaveInventoryCommand
        {
            get
            {
                return new Command(async () =>
                {                    
                    if (IsBusy == false)
                    {
                        IsBusy = true;
                        await SaveInventoryDetails();
                    }
                });

            }
        }

        public async Task SaveInventoryDetails()
        {
            try
            {
                IsSaveInventoryEnable = false;
                IsBusy = true;
                if (NetworkAvailable)
                {
                    bool IsAnyInvalidModelNo = lstSaveInventoryStockEntryRequest.Any(d => d.ProductSubCategoryId == 0);
                    if (IsAnyInvalidModelNo == true)
                    {
                        await DisplayAlert("Error!", "Please remove invalid model numbers.");
                        return;
                    }

                    InventoryManagementSL inventoryManagementSL = new InventoryManagementSL();
                    var receiveContext = await inventoryManagementSL.CreateInventoryStockEntry(lstSaveInventoryStockEntryRequest);
                    inventoryDataEntryDb.DeleteAllEntriesBasedDateStore(lstSaveInventoryStockEntryRequest.FirstOrDefault().LocationId, lstSaveInventoryStockEntryRequest.FirstOrDefault().EntryDate, (int)lstSaveInventoryStockEntryRequest.FirstOrDefault().StockEntryTypeId);
                    if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                    {
                        var StockEntry = lstSaveInventoryStockEntryRequest.FirstOrDefault().StockEntryTypeId;
                        if (StockEntry == (int)StockEntryTypeEnum.InventoryStockEntry)
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("Inventory"));
                        }
                        else
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("OutOfStock"));
                        }
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else if (receiveContext != null)
                    {
                        await ErrorDisplayAlert(receiveContext.ErrorMessage);
                    }
                    else
                    {
                        await ErrorDisplayAlert(Resx.AppResources.lblErrorTitle);
                    }
                }
                else
                {
                    showToasterNoNetwork();
                    if (lstSaveInventoryStockEntryRequest != null && lstSaveInventoryStockEntryRequest.Count != 0)
                    {
                        var CurrentDate = lstSaveInventoryStockEntryRequest.FirstOrDefault().EntryDate;
                        var LocationId = lstSaveInventoryStockEntryRequest.FirstOrDefault().LocationId;
                        var InventoryTypeId= lstSaveInventoryStockEntryRequest.FirstOrDefault().StockEntryTypeId;

                        inventoryDataEntryDb.DeleteAllEntriesBasedDateStore(LocationId, CurrentDate, (int)InventoryTypeId);

                        tblInventoryStockEntryRequest = new TblInventoryStockEntryRequest();
                        foreach (var item in lstSaveInventoryStockEntryRequest)
                        {
                            tblInventoryStockEntryRequest = new TblInventoryStockEntryRequest
                            {
                                ProductModelId = item.ProductModelId,
                                ProductCategoryId = item.ProductCategoryId,
                                ProductSubCategoryId = item.ProductSubCategoryId,
                                ProductCategoryName = item.ProductCategoryName,
                                ProductSubCategoryName = item.ProductSubCategoryName,
                                ProductModelNumber = item.ProductModelNumber,
                                EntryDate = item.EntryDate,
                                CreationDate = item.CreationDate,
                                Quantity = item.Quantity,
                                EntryByPersonId = item.EntryByPersonId,
                                LocationId = item.LocationId,
                                StockEntryTypeId = item.StockEntryTypeId,
                                CountryId = item.CountryId,
                                InventoryEntrySubmittedStatus = 1,
                            };

                            inventoryDataEntryDb.AddInventoryEntry(tblInventoryStockEntryRequest);
                        }

                        var StockEntry = lstSaveInventoryStockEntryRequest.FirstOrDefault().StockEntryTypeId;
                        if (StockEntry == (int)StockEntryTypeEnum.InventoryStockEntry)
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("Inventory"));
                        }
                        else
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("OutOfStock"));
                        }
                        await PopupNavigation.Instance.PopAsync();

                    }

                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    bool IsSubmitEnable = true;
                    MessagingCenter.Send<object, bool>(this, "AllowSubmitInventory", IsSubmitEnable);
                    await PopupNavigation.Instance.PopAsync();
                });

            }
        }

        private List<InventoryStockEntryRequest> _lstSaveInventoryStockEntryRequest;
        public List<InventoryStockEntryRequest> lstSaveInventoryStockEntryRequest
        {
            get { return _lstSaveInventoryStockEntryRequest; }
            set
            {
                _lstSaveInventoryStockEntryRequest = value;
                OnPropertyChanged("lstSaveInventoryStockEntryRequest");
            }
        }
        private string _TotalCount;
        public string TotalCount
        {
            get { return _TotalCount; }
            set
            {
                _TotalCount = value;
                OnPropertyChanged("TotalCount");
            }
        }

        private bool _IsSaveInventoryEnable;
        public bool IsSaveInventoryEnable
        {
            get { return _IsSaveInventoryEnable; }
            set
            {
                _IsSaveInventoryEnable = value;
                OnPropertyChanged(nameof(IsSaveInventoryEnable));
            }
        }
    }
}


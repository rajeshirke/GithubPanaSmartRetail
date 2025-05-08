using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class ConfirmSlesDataEntryPopupViewModel : BaseViewModel
    {
        public SalesDataEntryDb dataEntryDb;
        public int _flag { get; set; }
        Connection c;
        public TmpSalesDataEntry TmpSalesData;

        public ConfirmSlesDataEntryPopupViewModel(INavigation navigation,List<SalesEntryRequest> salesEntries,int flag):base(navigation)
        {
            IsEnableSaveSalesEntry = true;
            _flag = flag;                
            SaveSalesEntries = new List<SalesEntryRequest>();
            SaveSalesEntries = salesEntries;
            TotalEntries = SaveSalesEntries.Count;
            dataEntryDb = new SalesDataEntryDb();
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
        }

        public Command SaveSalesEntryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if(IsBusy == false)
                    {
                        IsBusy = true;
                        await SaveSalesDataEntries();
                    }
                });

            }
        }

        public Command GoBackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    bool IsSubmitEnable = true;
                    MessagingCenter.Send<object, bool>(this, "AllowSubmit", IsSubmitEnable);
                    await PopupNavigation.Instance.PopAsync();
                });

            }
        }

        public async Task SaveSalesDataEntries()
        {
            try
            {
                IsBusy = true;
                IsEnableSaveSalesEntry = false;
                if (NetworkAvailable)
                {
                    bool IsAnyInvalidModelNo = SaveSalesEntries.Any(d => d.ProductSubCategoryId == 0);
                    if (IsAnyInvalidModelNo == true)
                    {
                        await DisplayAlert("Error!", "Please remove invalid model nos.");
                        return;
                    }
                     
                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                    if (_flag == 1)
                    {
                        var receiveContext = await salesTargetManagementSL.SaveSalesDataEntries(SaveSalesEntries);
                        if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("SalesDataEntry"));
                            await PopupNavigation.Instance.PopAsync();
                            clearLocalDb();
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
                        var receiveContext = await salesTargetManagementSL.SaveSalesDataReturnEntries(SaveSalesEntries);
                        if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            await Navigation.PushAsync(new FeedBackSuccessPage("SalesDataReturnEntry"));
                            await PopupNavigation.Instance.PopAsync();

                            clearLocalDb();
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
                }
                else
                {
                    showToasterNoNetwork();
                    if (_flag == 1)
                    {
                        if (SaveSalesEntries != null && SaveSalesEntries.Count != 0)
                        {
                            var CurrentDate = SaveSalesEntries.FirstOrDefault().EntryDate;
                            var LocationId = SaveSalesEntries.FirstOrDefault().LocationId;

                            dataEntryDb.DeleteAllEntriesBasedDateStore(LocationId, CurrentDate);

                            foreach (var item in SaveSalesEntries)
                            {
                                TmpSalesData = new TmpSalesDataEntry
                                {
                                    CurrencyId = item.CurrencyId,
                                    ProductModelId = item.ProductModelId,
                                    ProductModelNumber = item.ProductModelNumber,
                                    EntryDate = item.EntryDate,
                                    Quantity = item.Quantity,
                                    UnitPrice = item.UnitPrice,
                                    TotalPrice = item.TotalPrice,
                                    EntryByPersonId = item.EntryByPersonId,
                                    ProductCategoryId = item.ProductCategoryId,
                                    ProductSubCategoryId = item.ProductSubCategoryId,
                                    ProductCategoryName = item.ProductCategoryName,
                                    ProductSubCategoryName = item.ProductSubCategoryName,
                                    LocationId = item.LocationId,
                                    TargetInOutStatusId = item.TargetInOutStatusId,
                                    CountryId = item.CountryId,
                                    SalesEntrySubmittedStatus = 1,
                                };

                                dataEntryDb.AddSalesEntry(TmpSalesData);
                            }

                            await Navigation.PushAsync(new FeedBackSuccessPage("SalesDataEntry"));
                            await PopupNavigation.Instance.PopAsync();
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0,null,ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void clearLocalDb()
        {
            var salesEntry = SaveSalesEntries.FirstOrDefault();
            dataEntryDb.DeleteAllEntriesBasedDateStore(salesEntry.LocationId, Convert.ToDateTime(salesEntry.EntryDate));
        }

        private List<SalesEntryRequest> _SaveSalesEntries;
        public List<SalesEntryRequest> SaveSalesEntries 
        {
            get { return _SaveSalesEntries; }
            set
            {
                _SaveSalesEntries = value;
                OnPropertyChanged("SaveSalesEntries");
            }
        }

        private int _TotalEntries;
        public int TotalEntries
        {
            get { return _TotalEntries; }
            set
            {
                _TotalEntries = value;
                OnPropertyChanged("TotalEntries");
            }
        }

        private bool _IsEnableSaveSalesEntry;
        public bool IsEnableSaveSalesEntry
        {
            get { return _IsEnableSaveSalesEntry; }
            set
            {
                _IsEnableSaveSalesEntry = value;
                OnPropertyChanged(nameof(IsEnableSaveSalesEntry));
            }
        }
    }
}


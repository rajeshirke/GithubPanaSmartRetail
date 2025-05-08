using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.CommonPages;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Internals;
using System.Diagnostics;
using Retail.DependencyServices;

namespace Retail.ViewModels.SalesTarget
{
    public class SalesDataReturnEntryViewModel : BaseViewModel
    {
        public SalesDataReturnEntryViewModel(INavigation navigation) : base(navigation)
        {
            lstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
            updatedlstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
            obSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
            SelectedDate = DateTime.Now;
            TotalCount = "0"; IsEnableSaveSalesEntry = true;
        }

        public Command GetSalesTransactionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsBusy = true;
                        if (NetworkAvailable)
                        {                            
                            lstSalesEntryRequest = new List<SalesEntryRequest>();
                            SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
            
                            string DateToSend = SelectedDate.ToString("yyyy-MM-dd");                        

                            lstSalesEntryRequest = await salesTargetManagementSL.GetSalesEntryTransactionsByDate(DateToSend, (int)TargetInOutStatusEnum.In, (long)CommonAttribute.CustomeProfile?.PersonId);
                            if(lstSalesEntryRequest!=null && lstSalesEntryRequest.Count!=0)
                            {
                                lstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>(lstSalesEntryRequest);
                                obSalesEntryRequest = new ObservableCollection<SalesEntryRequest>(lstSalesEntryRequest);
                              
                                ReturnEntriesHeight = lstAddSalesEntryRequest.Count * 20;
                                TotalCount = lstAddSalesEntryRequest.Count.ToString();
                            }
                            else
                            {
                                lstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
                                obSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
                                TotalCount = "0";
                            }
                            
                        }
                        else
                        {
                            showToasterNoNetwork();
                        }
                    }
                    catch(Exception ex)
                    {
                        Debugger.Log(0, null, ex.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });                
            }
        }

        public ICommand AddEntriesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (ModelNumber == string.Empty || ModelNumber == null)
                        {
                            await ErrorDisplayAlert("Please enter model number.");
                            return;
                        }
                        else if (Quantity == 0 || Quantity.ToString() == string.Empty || Quantity == null || (Quantity.ToString().Contains('-')))
                        {
                            await ErrorDisplayAlert("Please enter valid quantity.");
                            return;
                        }
                        else if (UnitPrice == 0 || UnitPrice.ToString() == string.Empty || UnitPrice == null)
                        {
                            await ErrorDisplayAlert("Please enter unit price.");
                            return;
                        }

                        if (obSalesEntryRequest != null && obSalesEntryRequest.Count != 0)
                        {
                            var Qty = obSalesEntryRequest.Where(m => m.ProductModelNumber == ModelNumber && m.UnitPrice == UnitPrice).FirstOrDefault();
                            if (Qty != null)
                            {
                                if((int)Quantity > Qty.Quantity)
                                {
                                    await ErrorDisplayAlert("Quantity should not be greater than the available quantity.");
                                    return;
                                }
                            }
                        }

                        try
                        {
                            var productmodel = lstSalesEntryRequest.Where(x => x.ProductModelNumber == ModelNumber).FirstOrDefault();
                            var SalesEntryRequest1 = new SalesEntryRequest
                            {
                                ProductModelNumber = ModelNumber,
                                EntryDate = SelectedDate,
                                Quantity = (int)Quantity,
                                UnitPrice = (decimal)UnitPrice,
                                TotalPrice = Convert.ToDecimal(UnitPrice * Quantity),
                                EntryByPersonId = CommonAttribute.CustomeProfile.PersonId,
                                ProductCategoryId = productmodel.ProductCategoryId,
                                LocationId = productmodel.LocationId,
                                TargetInOutStatusId = (int)TargetInOutStatusEnum.Out,
                                CurrencyId = productmodel.CurrencyId,
                                ProductModelId = productmodel.ProductModelId,
                                ProductSubCategoryId = productmodel.ProductSubCategoryId,
                                CountryId = CommonAttribute.CustomeProfile?.CountryId, 
                                SelectedReturn = true
                            };
                            updatedlstAddSalesEntryRequest.Add(SalesEntryRequest1);
                            lstAddSalesEntryRequest.Add(SalesEntryRequest1);
                            lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                            UpdatedReturnEntriesHeight = updatedlstAddSalesEntryRequest.Count * 20;
                            Quantity = null;UnitPrice = null;
                            //TotalAmount = TotalAmount - (Convert.ToDouble(Quantity) * Convert.ToDouble(UnitPrice));
                        }
                        catch(Exception c)
                        { }

                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new Command<SalesEntryRequest>(async (item) =>
                {

                    ModelNumber = item.ProductModelNumber;
                    Quantity = item.Quantity;
                    UnitPrice = item.UnitPrice;
                    //TotalAmount = TotalAmount - (Convert.ToDouble(item.Quantity) * Convert.ToDouble(item.UnitPrice));

                    lstAddSalesEntryRequest.Remove(item);
                    lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                    TotalCount = lstAddSalesEntryRequest.Count.ToString();
                    ReturnEntriesHeight= lstAddSalesEntryRequest.Count * 20;
                });

            }
        }

        //UpdatedEditCommand

        public Command ConfirmSalesReturnCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsBusy = true;

                        if (NetworkAvailable)
                        {
                            IsEnableSaveSalesEntry = false;
                            if (updatedlstAddSalesEntryRequest != null && updatedlstAddSalesEntryRequest.Count != 0)
                            {
                                updatedlstAddSalesEntryRequest.Clear();
                                var UpdatedReturnEntries = lstAddSalesEntryRequest.Where(d => d.SelectedReturn == true).ToList();
                                updatedlstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>(UpdatedReturnEntries);
                                lstSalesEntryRequest = new List<SalesEntryRequest>(updatedlstAddSalesEntryRequest);
                                ConfirmSalesEntryData = new List<SalesEntryRequest>();
                                SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                                ConfirmSalesEntryData = await salesTargetManagementSL.BuildSalesEntryReuqestWithModelDetails(lstSalesEntryRequest);
                                if (ConfirmSalesEntryData.Count != 0)
                                    await PopupNavigation.Instance.PushAsync(new ConfirmSlesDataEntryPopup(ConfirmSalesEntryData, 2));
                            }
                            else
                            {
                                IsEnableSaveSalesEntry = true;
                                await ErrorDisplayAlert("You have not updated any entries.");
                                return;
                            }
                        }
                        else
                        {
                            showToasterNoNetwork();
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
                });

            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();   
                });

            }
        }



        private int _ReturnEntriesHeight;
        public int ReturnEntriesHeight
        {
            get { return _ReturnEntriesHeight; }
            set
            {
                _ReturnEntriesHeight = value;
                OnPropertyChanged("ReturnEntriesHeight");
            }
        }

        private int _UpdatedReturnEntriesHeight;
        public int UpdatedReturnEntriesHeight
        {
            get { return _UpdatedReturnEntriesHeight; }
            set
            {
                _UpdatedReturnEntriesHeight = value;
                OnPropertyChanged("UpdatedReturnEntriesHeight");
            }
        }

        private List<SalesEntryRequest> _ConfirmSalesEntryData;
        public List<SalesEntryRequest> ConfirmSalesEntryData
        {
            get { return _ConfirmSalesEntryData; }
            set
            {
                _ConfirmSalesEntryData = value;
                OnPropertyChanged("ConfirmSalesEntryData");
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

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private string _ModelNumber;
        public string ModelNumber

        {
            get { return _ModelNumber; }
            set
            {
                _ModelNumber = value;
                OnPropertyChanged("ModelNumber");
            }
        }

        private decimal _TotalAmount;
        public decimal TotalAmount

        {
            get {
                _TotalAmount = 0;
                if (lstAddSalesEntryRequest != null)
                {
                    lstAddSalesEntryRequest.ForEach(data =>
                    {
                        _TotalAmount = _TotalAmount + data.TotalPrice;
                    });
                }
                return _TotalAmount;
            }
        }

        private int? _Quantity;
        public int? Quantity

        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private decimal? _UnitPrice;
        public decimal? UnitPrice

        {
            get { return _UnitPrice; }
            set
            {
                _UnitPrice = value;
                OnPropertyChanged("UnitPrice");
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

        private ObservableCollection<SalesEntryRequest> _updatedlstAddSalesEntryRequest;
        public ObservableCollection<SalesEntryRequest> updatedlstAddSalesEntryRequest
        {
            get { return _updatedlstAddSalesEntryRequest; }
            set
            {
                _updatedlstAddSalesEntryRequest = value;
                OnPropertyChanged("updatedlstAddSalesEntryRequest");
            }
        }
        private ObservableCollection<SalesEntryRequest> _lstAddSalesEntryRequest;
        public ObservableCollection<SalesEntryRequest> lstAddSalesEntryRequest
        {
            get { return _lstAddSalesEntryRequest; }
            set
            {
                _lstAddSalesEntryRequest = value;
                OnPropertyChanged(nameof(lstAddSalesEntryRequest));
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private ObservableCollection<SalesEntryRequest> _obSalesEntryRequest;
        public ObservableCollection<SalesEntryRequest> obSalesEntryRequest
        {
            get { return _obSalesEntryRequest; }
            set
            {
                _obSalesEntryRequest = value;
                OnPropertyChanged(nameof(obSalesEntryRequest));                
            }
        }

        private List<SalesEntryRequest> _lstSalesEntryRequest;
        public List<SalesEntryRequest> lstSalesEntryRequest
        {
            get { return _lstSalesEntryRequest; }
            set
            {
                _lstSalesEntryRequest = value;
                OnPropertyChanged("lstSalesEntryRequest");
            }
        }

    }
}

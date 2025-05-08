    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Controls;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
 
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class SalesDataEntryViewModel : BaseViewModel
    {

        Connection c;
        public SalesDataEntryDb SalesDataEntryDb;
        public TmpSalesDataEntry TmpSalesData;
        public LocationStoreDb locationStoreDb;
        public int CategoryId = 0; public int SubCategoryId = 0; public long ModelId = 0;
        public string CategoryName = ""; public string SubCategoryName = "";
        public bool IsExpanded { get; set; }

        public SalesDataEntryViewModel(INavigation navigation):base(navigation)
        {
            
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            //SelectedDate = DateTime.Now.ToString("dd,MMMM,yyyy");           
            lstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>();
            TotalCount = "0";
            SalesDataEntryDb = new SalesDataEntryDb();
            locationStoreDb = new LocationStoreDb();
            IsExpanded = true; IsEnableSaveSalesEntry = true;
        }

        public async Task GetLocation()
        {
            StoreDropDown = new ObservableCollection<DropDownModel>();

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (Locations.Count != 0)
                    {
                        foreach (var item in Locations)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        var list = StoreDropDown.OrderBy(snm => snm.Title);
                        StoreDropDown = new ObservableCollection<DropDownModel>(list);
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
                            if (!gpsStatus)
                                await ErrorDisplayAlert("Your gps location is not accurate");
                        }
                    }
                }
                else
                {
                   
                    var tblLocationStores = (List<TblLocationStore>)locationStoreDb.GetlocationStores().ToList();

                    if (tblLocationStores != null && tblLocationStores.Count() > 0)
                    {
                        foreach (var item in tblLocationStores)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        var list = StoreDropDown.OrderBy(snm => snm.Title);
                        StoreDropDown = new ObservableCollection<DropDownModel>(list);
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

        public ICommand AddEntriesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        //if (Device.RuntimePlatform == Device.Android)
                        //{
                        //    var dateTimeSettings = DependencyService.Get<IDateTimeSettings>();
                        //    bool isAutomaticDateTimeEnabled = dateTimeSettings.IsAutomaticDateTimeEnabled();

                        //    if (!isAutomaticDateTimeEnabled)
                        //    {
                        //        // Automatic date and time are not enabled
                        //        await DisplayAlert("Error!", "Please enable automatic date and time settings in your device's settings to use this application properly.");
                        //        return;
                        //    }
                        //}

                        try
                        {
                            string pricevalue = "0";
                            int SelectedStoreID = 0;
                            if (SelectedStore != null)
                            {
                                SelectedStoreID = SelectedStore.Id;
                            }
                            if (SelectedStoreID == 0)
                            {
                                await ErrorDisplayAlert("Please select store.");
                                return;
                            }
                            if (string.IsNullOrEmpty(SelectedDate))
                            {
                                await ErrorDisplayAlert("Please select date.");
                                return;
                            }
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
                            else if (UnitPrice == "0" || UnitPrice.ToString() == string.Empty || UnitPrice.StartsWith("00") || UnitPrice.StartsWith("0.0") || UnitPrice.Contains('-'))
                            {
                                await ErrorDisplayAlert("Please enter valid unit price.");
                                return;
                            }

                            if (lstAddSalesEntryRequest != null)
                            {
                                foreach (var item in lstAddSalesEntryRequest)
                                {
                                    if (item.ProductModelNumber == ModelNumber && item.Quantity == Quantity && item.UnitPrice == Convert.ToDecimal(UnitPrice))
                                    {
                                        await ErrorDisplayAlert("Entry already exists.");
                                        return;
                                    }
                                }
                            }

                            if (NetworkAvailable)
                            {
                                SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                                bool ValidModelNumber = await salesTargetManagementSL.ValidateModelByModelNumberCountry((int)CommonAttribute.CustomeProfile?.CountryId, ModelNumber);
                                if (ValidModelNumber == true)
                                {
                                    try
                                    {
                                        lstAddSalesEntryRequest.Add(new SalesEntryRequest
                                        {
                                            CurrencyId = CommonAttribute.CustomeProfile?.CountryResponse?.CurrencyId,
                                            ProductModelId = 1,
                                            ProductModelNumber = ModelNumber,
                                            EntryDate = Convert.ToDateTime(SelectedDate),
                                            Quantity = (int)Quantity,
                                            //UnitPrice = (decimal)UnitPrice,
                                            //TotalPrice = Convert.ToDecimal(UnitPrice * Quantity),
                                            UnitPrice = Convert.ToDecimal(UnitPrice),
                                            TotalPrice = Convert.ToDecimal(Convert.ToDecimal(UnitPrice) * Quantity),
                                            EntryByPersonId = (long)CommonAttribute.CustomeProfile?.PersonId,
                                            ProductCategoryId = 0,
                                            LocationId = SelectedStoreID,
                                            TargetInOutStatusId = (int)TargetInOutStatusEnum.In,
                                            CountryId = CommonAttribute.CustomeProfile?.CountryId,


                                        });
                                        lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                                        //TotalAmount = TotalAmount + (Convert.ToDouble(Quantity) * Convert.ToDouble(UnitPrice));
                                        TotalCount = lstAddSalesEntryRequest.Count.ToString();
                                        //Quantity = 0; UnitPrice = 0;
                                        Quantity = null; UnitPrice = null;
                                        ModelNumber = "";

                                        addLocalEntryList();
                                    }

                                    catch (Exception ex)
                                    {
                                        Debugger.Log(0, null, ex.ToString());
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Error!", "Invalid model number");
                                }
                            }
                            else
                            {
                                try
                                {
                                    var ValidModelNo = c.conn.Table<TblAllModelNumbers>().ToList();
                                    if (ValidModelNo != null && ValidModelNo.Count != 0)
                                    {
                                        var ModelNo = ValidModelNo.Where(s => s.ProductModelNumber.ToLower() == ModelNumber.ToLower()).ToList();
                                        if (ModelNo != null && ModelNo.Count == 0)
                                        {
                                            await ErrorDisplayAlert("Invalid model number!");
                                            return;
                                        }
                                    }


                                    lstAddSalesEntryRequest.Add(new SalesEntryRequest
                                    {
                                        CurrencyId = CommonAttribute.CustomeProfile?.CountryResponse?.CurrencyId,
                                        ProductModelId = 1,
                                        ProductModelNumber = ModelNumber,
                                        EntryDate = Convert.ToDateTime(SelectedDate),
                                        Quantity = (int)Quantity,
                                        //UnitPrice = (decimal)UnitPrice,
                                        //TotalPrice = Convert.ToDecimal(UnitPrice * Quantity),
                                        UnitPrice = Convert.ToDecimal(UnitPrice),
                                        TotalPrice = Convert.ToDecimal(Convert.ToDecimal(UnitPrice) * Quantity),
                                        EntryByPersonId = (long)CommonAttribute.CustomeProfile?.PersonId,
                                        ProductCategoryId = 0,
                                        LocationId = SelectedStoreID,
                                        TargetInOutStatusId = (int)TargetInOutStatusEnum.In,
                                        CountryId = CommonAttribute.CustomeProfile?.CountryId,


                                    });
                                    lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                                    TotalCount = lstAddSalesEntryRequest.Count.ToString();
                                    Quantity = null; UnitPrice = null;
                                    ModelNumber = "";

                                    addLocalEntryList();
                                }

                                catch (Exception ex)
                                {
                                    Debugger.Log(0, null, ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    
                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command<SalesEntryRequest>(async (item) =>
                {
                    var answer = await YesorNoDisplayAlert("Alert", "Do you want to delete entry?");
                    if(answer)
                    {
                        lstAddSalesEntryRequest.Remove(item);
                        lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                        //TotalAmount = TotalAmount - (Convert.ToDouble(item.Quantity) * Convert.ToDouble(item.UnitPrice));
                        TotalCount = lstAddSalesEntryRequest.Count.ToString();

                        addLocalEntryList();
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

        public ICommand EditCommand
        {
            get
            {
                return new Command<SalesEntryRequest>(async (item) =>
                {

                    ModelNumber = item.ProductModelNumber;
                    Quantity = item.Quantity;
                    //UnitPrice = item.UnitPrice;
                    UnitPrice = item.UnitPrice.ToString();
                    //TotalAmount = TotalAmount - (Convert.ToDouble(item.Quantity) * Convert.ToDouble(item.UnitPrice));
                    lstAddSalesEntryRequest.Remove(item);
                    lstAddSalesEntryRequest = lstAddSalesEntryRequest;
                    TotalCount = lstAddSalesEntryRequest.Count.ToString();

                    addLocalEntryList();
                });
            }
        }

        public Command SearchByCategoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new ProductCategoryPopupView(1));
                });
            }
        }

        public Command RecentlyUsedModelNoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new RecentlyUsedModelNoPopupView(2));

                });
            }
        }

        public Command ConfirmSalesEntryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsBusy = true;
                        //if (Device.RuntimePlatform == Device.Android)
                        //{
                        //    var dateTimeSettings = DependencyService.Get<IDateTimeSettings>();
                        //    bool isAutomaticDateTimeEnabled = dateTimeSettings.IsAutomaticDateTimeEnabled();

                        //    if (!isAutomaticDateTimeEnabled)
                        //    {
                        //        // Automatic date and time are not enabled
                        //        await DisplayAlert("Error!", "Please enable automatic date and time settings in your device's settings to use this application properly.");
                        //        return;
                        //    }
                        //}
                        if (NetworkAvailable)
                        {
                            IsEnableSaveSalesEntry = false;
                            if (lstAddSalesEntryRequest != null && lstAddSalesEntryRequest.Count != 0)
                            {
                                    lstSalesEntryRequest = new List<SalesEntryRequest>(lstAddSalesEntryRequest);
                                    ConfirmSalesEntryData = new List<SalesEntryRequest>();
                                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                                    ConfirmSalesEntryData = await salesTargetManagementSL.BuildSalesEntryReuqestWithModelDetails(lstSalesEntryRequest);
                                    if (ConfirmSalesEntryData != null && ConfirmSalesEntryData.Count != 0)
                                        await PopupNavigation.Instance.PushAsync(new ConfirmSlesDataEntryPopup(ConfirmSalesEntryData, 1));
                              
                            }
                            else
                            {
                                IsEnableSaveSalesEntry = true;
                                await ErrorDisplayAlert("Please add entries");
                                return;
                            }
                        }
                        else
                        {
                            IsEnableSaveSalesEntry = false;
                            ConfirmSalesEntryData = new List<SalesEntryRequest>();
                            DateTime SalesDate = Convert.ToDateTime(SelectedDate).Date;
                            var OfflineSalesEntries = c.conn.Table<TmpSalesDataEntry>().Where(d => d.EntryDate == SalesDate && d.LocationId == SelectedStore.Id).ToList();
                            if (OfflineSalesEntries != null && OfflineSalesEntries.Count != 0)
                            {
                                foreach (var item in OfflineSalesEntries)
                                {
                                    ConfirmSalesEntryData.Add(new SalesEntryRequest
                                    {
                                        CountryId = item.CountryId,
                                        CurrencyId = item?.CurrencyId,
                                        ProductModelId = item.ProductModelId,
                                        ProductCategoryId = item.ProductCategoryId,
                                        ProductSubCategoryId = item.ProductSubCategoryId,
                                        ProductCategoryName = item.ProductCategoryName,
                                        ProductSubCategoryName = item.ProductSubCategoryName,
                                        ProductModelNumber = item.ProductModelNumber,
                                        EntryDate = item.EntryDate,
                                        Quantity = item.Quantity,
                                        UnitPrice = item.UnitPrice,
                                        TotalPrice = item.TotalPrice,
                                        EntryByPersonId = item.EntryByPersonId,
                                        LocationId = item.LocationId,
                                        TargetInOutStatusId = item.TargetInOutStatusId,
                                        SalesEntrySubmittedStatus = (long)item.SalesEntrySubmittedStatus,

                                    });
                                }

                                if (ConfirmSalesEntryData != null && ConfirmSalesEntryData.Count != 0)
                                    await PopupNavigation.Instance.PushAsync(new ConfirmSlesDataEntryPopup(ConfirmSalesEntryData, 1));
                            }
                            else
                            {
                                IsEnableSaveSalesEntry = true;
                                await ErrorDisplayAlert("Please add entries");
                                return;
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
                });

            }
        }

        public Command SelectStoreCommand
        {
            get
            {
                return new Command<DropDownModel>((obj) =>
                {
                    if (obj != null)
                        SelectedStore = obj;
                    else
                        SelectedStore.Id = 0;
                });
            }
        }

        public async Task ValidateModelNumber()
        {
            if (NetworkAvailable)
            {
                MasterDataManagementSL dataManagementSL = new MasterDataManagementSL();
                if (!string.IsNullOrEmpty(ModelNumber) && ModelNumber.Length > 3)
                {
                    ModelNumberValide = false;

                    return;
                }
                long CountryID = (long)CommonAttribute.CustomeProfile?.CountryId;
                List<string> modelNumberSearchResponses = await dataManagementSL.SearchModelNumberByInitials(ModelNumber, CountryID);
                if (modelNumberSearchResponses != null)
                {
                    var exstingItem = modelNumberSearchResponses.Where(u => u.ToLower() == ModelNumber.ToLower()).ToList();
                    if (exstingItem.Count > 0)
                        ModelNumberValide = true;
                    else
                        ModelNumberValide = false;
                }
            }
            else
            {
                await OfflineSearchModelNos();
            }
            
        }

        public async Task OfflineSearchModelNos()
        {
            try
            {
                var allModelNumbers = c.conn.Table<TblAllModelNumbers>().ToList();
                List<string> modelNumberSearchResponses = new List<string>();
                modelNumberSearchResponses = new List<string>(allModelNumbers.Select(c => c.ProductModelNumber).Where(m => m.ToLower().StartsWith(ModelNumber.ToLower())).ToList());
                if (modelNumberSearchResponses != null)
                {
                    var exstingItem = modelNumberSearchResponses.Where(u => u.ToLower() == ModelNumber.ToLower()).ToList();
                    if (exstingItem.Count > 0)
                        ModelNumberValide = true;
                    else
                        ModelNumberValide = false;
                }
            }
            catch(Exception ex)
            {

            }
        }

        public Command ModelsPopupCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if(NetworkAvailable)
                    {
                        IsBusy = true;
                        DropDownPopupPage dropDownPopup = new DropDownPopupPage();
                        dropDownPopup.Title = "Model Number";

                        if (!string.IsNullOrEmpty(ModelNumber))
                        {
                            dropDownPopup.SetSearchKey(ModelNumber);
                            await dropDownPopup.GetModelNumber(ModelNumber);
                        }
                       
                        dropDownPopup.DropDownSelectedItem += DropDownPopup_DropDownSelectedItem;
                        IsBusy = false;
                        await PopupNavigation.Instance.PushAsync(dropDownPopup);
                    }
                    else
                    {
                        //showToasterNoNetwork();
                        IsBusy = true;
                        DropDownPopupPage dropDownPopup = new DropDownPopupPage();
                        dropDownPopup.Title = "Model Number";

                        if (!string.IsNullOrEmpty(ModelNumber))
                        {
                            dropDownPopup.SetSearchKey(ModelNumber);
                            await dropDownPopup.GetModelNumber(ModelNumber);
                        }

                        dropDownPopup.DropDownSelectedItem += DropDownPopup_DropDownSelectedItem;
                        IsBusy = false;
                        await PopupNavigation.Instance.PushAsync(dropDownPopup);
                    }
                });
            }
        }

        private void DropDownPopup_DropDownSelectedItem(object sender, EventArgs e)
        {
            DropDownPopupPage control = sender as DropDownPopupPage;
            ModelNumber = control.SelectedItem.Title.ToUpper();
            //  ModelNumberValide=true
            ModelNumberValide = true;
            modelNumberSearchResponse = new ModelNumberSearchResponse() { ProductClassificationId = control.SelectedItem.Id, ModelName = control.SelectedItem.Title, ModelDescreption = control.SelectedItem.Desc };

        }

        public ModelNumberSearchResponse modelNumberSearchResponse { get; set; }       
       

        private bool _ModelNumberValide;
        public bool ModelNumberValide
        {
            get { return _ModelNumberValide; }
            set
            {

                _ModelNumberValide = value;
                OnPropertyChanged(nameof(ModelNumberValide));
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

        private List<EntityKeyValueResponse> _Locations;
        public List<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private ObservableCollection<DropDownModel> _StoreDropDown;
        public ObservableCollection<DropDownModel> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged(nameof(StoreDropDown));
            }
        }

        private List<EntityKeyValueResponse> _lstLocations;
        public List<EntityKeyValueResponse> lstLocations
        {
            get { return _lstLocations; }
            set
            {
                _lstLocations = value;
                OnPropertyChanged("Locations");
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


        private DropDownModel _SelectedStore;
        public DropDownModel SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
                getLocalEntryList();
            }
        }

        private string _SelectedDate;
        public string SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                getLocalEntryList();
            }
        }

        private bool _SelectedDateVisible;
        public bool SelectedDateVisible
        {
            get { return _SelectedDateVisible; }
            set
            {
                _SelectedDateVisible = value;
                OnPropertyChanged(nameof(SelectedDateVisible));
            }
        }

        private List<SalesEntryRequest> _lstSalesEntryRequest;
        public List<SalesEntryRequest> lstSalesEntryRequest
        {
            get { return _lstSalesEntryRequest; }
            set
            {
                _lstSalesEntryRequest = value;
                OnPropertyChanged(nameof(lstSalesEntryRequest));
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

        public void getLocalEntryList() {
            try
            {
                lstAddSalesEntryRequest.Clear();
                lstAddSalesEntryRequest = lstAddSalesEntryRequest;

                

                Device.BeginInvokeOnMainThread(() => {
                    #region local db check
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }

                    var saleEntryLocalList = (List<TmpSalesDataEntry>)SalesDataEntryDb.GetSalesEntrysBasedDateStore(SelectedStoreID, Convert.ToDateTime(SelectedDate));
                    if (saleEntryLocalList != null && saleEntryLocalList.Count > 0)
                    {
                        List<SalesEntryRequest> salesEntryListRequests = new List<SalesEntryRequest>();
                        foreach (var item in saleEntryLocalList)
                        {
                            var ModelDetail = c.conn.Table<TblAllModelNumbers>().FirstOrDefault(m => m.ProductModelNumber == item.ProductModelNumber);
                            if (ModelDetail != null)
                            {
                                ModelId = ModelDetail.ProductModelId;
                                CategoryId = ModelDetail.ProductCategoryId;
                                SubCategoryId = ModelDetail.ProductSubCategoryId;
                                CategoryName = ModelDetail.Category;
                                SubCategoryName = ModelDetail.SubCategory;
                            }

                            salesEntryListRequests.Add(new SalesEntryRequest {
                                CurrencyId = item.CurrencyId,
                                ProductModelId = ModelId,
                                ProductModelNumber = item.ProductModelNumber,
                                EntryDate = item.EntryDate,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                TotalPrice = item.TotalPrice,
                                EntryByPersonId = item.EntryByPersonId,
                                ProductCategoryId = CategoryId,
                                ProductSubCategoryId = SubCategoryId,
                                ProductCategoryName = CategoryName,
                                ProductSubCategoryName = SubCategoryName,
                                LocationId = item.LocationId,
                                TargetInOutStatusId = item.TargetInOutStatusId,
                                CountryId=item.CountryId,
                                SalesEntrySubmittedStatus=(int)item.SalesEntrySubmittedStatus,
                            });
                        }
                        lstAddSalesEntryRequest = new ObservableCollection<SalesEntryRequest>(salesEntryListRequests);
                    }
                    //SelectedDate
                    //SelectedStore
                    #endregion
                });
            }
            catch (Exception ex)
            {
                Debugger.Log(0,null,ex.ToString());
            }
        }

        public void addLocalEntryList()
        {
            try
            {
                #region sales entry local db
                int SelectedStoreID = 0;
                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                SalesDataEntryDb.DeleteAllEntriesBasedDateStore(SelectedStoreID, Convert.ToDateTime(SelectedDate));

                if (lstAddSalesEntryRequest != null && lstAddSalesEntryRequest.Count > 0)
                {   
                    foreach (var item in lstAddSalesEntryRequest)
                    {
                        var ModelDetail = c.conn.Table<TblAllModelNumbers>().FirstOrDefault(m => m.ProductModelNumber == item.ProductModelNumber);

                        if(ModelDetail!=null)
                        {
                            ModelId = ModelDetail.ProductModelId;
                            CategoryId = ModelDetail.ProductCategoryId;
                            SubCategoryId = ModelDetail.ProductSubCategoryId;
                            CategoryName = ModelDetail.Category;
                            SubCategoryName = ModelDetail.SubCategory;
                        }

                        TmpSalesData = new TmpSalesDataEntry
                        {
                            CurrencyId = item.CurrencyId,
                            ProductModelId = ModelId,
                            ProductModelNumber = item.ProductModelNumber,
                            EntryDate = item.EntryDate,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.TotalPrice,
                            EntryByPersonId = item.EntryByPersonId,
                            ProductCategoryId = CategoryId,
                            ProductSubCategoryId=SubCategoryId,
                            ProductCategoryName=CategoryName,
                            ProductSubCategoryName=SubCategoryName,
                            LocationId = item.LocationId,
                            TargetInOutStatusId = item.TargetInOutStatusId,
                            CountryId=item.CountryId,
                            SalesEntrySubmittedStatus=0,
                        };

                        SalesDataEntryDb.AddSalesEntry(TmpSalesData);
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
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

        private string _ModelNumber;
        public string ModelNumber

        {
            get { return _ModelNumber; }
            set
            {
                ModelNumberValide = true;
                _ModelNumber = value;
                OnPropertyChanged("ModelNumber");
            }
        }

        private double _TotalAmount;
        public double TotalAmount

        {
            get {
                if (lstAddSalesEntryRequest != null &&
                    lstAddSalesEntryRequest.Count() > 0)
                {
                    _TotalAmount = 0;
                    foreach (var item in lstAddSalesEntryRequest)
                    {
                        _TotalAmount = _TotalAmount + (Convert.ToDouble(item.Quantity) * Convert.ToDouble(item.UnitPrice));
                    }
                }
                else
                {
                    _TotalAmount = 0;
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
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _UnitPrice;
        public string UnitPrice

        {
            get { return _UnitPrice; }
            set
            {
                _UnitPrice = value;
                OnPropertyChanged(nameof(UnitPrice));
            }
        }


        private int _SelectedLocationId;
        public int SelectedLocationId

        {
            get { return _SelectedLocationId; }
            set
            {
                _SelectedLocationId = value;
                OnPropertyChanged(nameof(SelectedLocationId));
            }
        }
    }
}

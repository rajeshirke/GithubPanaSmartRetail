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
using Retail.Views.InventoryStock;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.InventoryStock
{
    public class ADdInventoryViewModel : BaseViewModel
    {
        Connection c;
        public int CategoryId = 0; public int SubCategoryId = 0; public long ModelId = 0;
        public string CategoryName = ""; public string SubCategoryName = "";
        public TblInventoryStockEntryRequest tblInventoryStockEntryRequest = new TblInventoryStockEntryRequest();
        public InventoryDataEntryDb inventoryDataEntryDb;
        public TblInventoryStockEntryRequest tblInventoryStock;
        public LocationStoreDb locationStoreDb;

        public ADdInventoryViewModel(INavigation navigation) : base(navigation)
        {
                        
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            lstInventoryStockEntryRequest = new List<InventoryStockEntryRequest>();
            obInventoryStockEntryRequest = new ObservableCollection<InventoryStockEntryRequest>();
            if (lstInventoryStockEntryRequest != null)
                TotalCount = lstInventoryStockEntryRequest.Count.ToString();
            else
                TotalCount = "0";
            SelectedDate = DateTime.Now.ToString("dd,MMMM,yyyy");

            EntryDateEnable = true; IsEnableInventoryEntry = true;
            inventoryDataEntryDb = new InventoryDataEntryDb();
            locationStoreDb = new LocationStoreDb();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command<InventoryStockEntryRequest>(async (item) =>
                {
                    var answer = await YesorNoDisplayAlert("Alert", "Do you want to delete entry?");
                    if (answer)
                    {
                        obInventoryStockEntryRequest.Remove(item);
                        TotalCount = obInventoryStockEntryRequest.Count.ToString();
                        addLocalInventoryEntryList();
                    }                        
                });
            }
        }

        public ICommand ConfirmCommand
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
                            IsEnableInventoryEntry = false;
                            if (obInventoryStockEntryRequest != null && obInventoryStockEntryRequest.Count != 0)
                            {
                                InventoryManagementSL inventoryManagementSL = new InventoryManagementSL();
                                lstInventoryStockEntryRequest = new List<InventoryStockEntryRequest>(obInventoryStockEntryRequest);
                                lstInventoryStockEntryRequest = await inventoryManagementSL.BuildStockEntryWithModelDetails(lstInventoryStockEntryRequest);
                                if (lstInventoryStockEntryRequest!=null && lstInventoryStockEntryRequest.Count != 0)
                                    await PopupNavigation.Instance.PushAsync(new ConfirmInventoryPopup(lstInventoryStockEntryRequest));
                            }
                            else
                            {
                                IsEnableInventoryEntry = true;
                                await ErrorDisplayAlert("Please add entries.");
                                return;
                            }
                        }
                        else
                        {
                            IsEnableInventoryEntry = false;
                            lstInventoryStockEntryRequest = new List<InventoryStockEntryRequest>();
                            DateTime InventoryDate = Convert.ToDateTime(SelectedDate).Date;
                            var OfflineInventoryEntries = c.conn.Table<TblInventoryStockEntryRequest>().Where(d => d.EntryDate == InventoryDate && d.LocationId == SelectedStore.Id && d.StockEntryTypeId == InventoryFlag).ToList();
                            if (OfflineInventoryEntries != null && OfflineInventoryEntries != null)
                            {
                                foreach(var item in OfflineInventoryEntries)
                                {
                                    lstInventoryStockEntryRequest.Add(new InventoryStockEntryRequest
                                    {
                                        InventoryStockEntryId =(int)item.InventoryStockEntryId,
                                        ProductModelId =item.ProductModelId,
                                        ProductModelNumber =item.ProductModelNumber,
                                        ProductCategoryId =item.ProductCategoryId,
                                        ProductCategoryName =item.ProductCategoryName,
                                        ProductSubCategoryId =item.ProductSubCategoryId,
                                        ProductSubCategoryName =item.ProductSubCategoryName,
                                        LocationId =item.LocationId,
                                        EntryDate =item.EntryDate,
                                        EntryByPersonId =item.EntryByPersonId,
                                        Comments =item.Comments,
                                        CreationDate =item.CreationDate,
                                        Quantity =item.Quantity,
                                        StockEntryTypeId =item.StockEntryTypeId,
                                        CountryId =item.CountryId,
                                        InventoryEntrySubmittedStatus=item.InventoryEntrySubmittedStatus,

                                    });
                                }

                                if (lstInventoryStockEntryRequest != null && lstInventoryStockEntryRequest.Count != 0)
                                    await PopupNavigation.Instance.PushAsync(new ConfirmInventoryPopup(lstInventoryStockEntryRequest));
                            }
                            else
                            {
                                IsEnableInventoryEntry = true;
                                await ErrorDisplayAlert("Please add entries.");
                                return;
                            }
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

        public ICommand AddInventoryStock
        {
            get
            {
                return new Command(async () =>
                {
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
                    if (ModelNumber == string.Empty || ModelNumber == null)
                    {
                        await ErrorDisplayAlert("Please enter model number.");
                        return;
                    }
                    if (IsOutofStock == false)
                    {
                        if (Quantity == 0 || Quantity.ToString() == string.Empty || Quantity==null || (Quantity.ToString().Contains('-')))
                        {
                            await ErrorDisplayAlert("Please enter valid quantity.");
                            return;
                        }
                    }

                    if (obInventoryStockEntryRequest != null)
                    {
                        foreach (var item in obInventoryStockEntryRequest)
                        {
                            if (IsOutofStock == false)
                            {
                                if (item.ProductModelNumber == ModelNumber && item.Quantity == Quantity)
                                {
                                    await ErrorDisplayAlert("Entry already exists.");
                                    return;
                                }
                            }
                            else
                            {
                                if (item.ProductModelNumber == ModelNumber && item.Quantity == 0)
                                {
                                    await ErrorDisplayAlert("Entry already exists.");
                                    return;
                                }
                            }
                        }
                    }

                    try
                    {
                        if(NetworkAvailable)
                        {
                            SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                            bool ValidModelNumber = await salesTargetManagementSL.ValidateModelByModelNumberCountry((int)CommonAttribute.CustomeProfile?.CountryId, ModelNumber);
                            if (ValidModelNumber == true)
                            {
                                obInventoryStockEntryRequest.Add(new InventoryStockEntryRequest
                                {
                                    ProductModelId = 1,
                                    ProductSubCategoryId = 0,
                                    ProductModelNumber = ModelNumber,
                                    EntryDate = Convert.ToDateTime(SelectedDate),
                                    CreationDate = Convert.ToDateTime(SelectedDate),
                                    Quantity = (IsOutofStock == true ? (int)0 : (int)Quantity),
                                    EntryByPersonId = (long)CommonAttribute.CustomeProfile?.PersonId,
                                    ProductCategoryId = 0,
                                    LocationId = SelectedStoreID,
                                    StockEntryTypeId = InventoryFlag,
                                    CountryId = CommonAttribute.CustomeProfile?.CountryId

                                });

                                TotalCount = obInventoryStockEntryRequest.Count.ToString();
                                addLocalInventoryEntryList();
                                Quantity = null; ModelNumber = "";
                            }
                            else
                            {
                                await DisplayAlert("Error!", "Invalid model number");
                            }
                        }
                        else
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

                            obInventoryStockEntryRequest.Add(new InventoryStockEntryRequest
                            {
                                ProductModelId = 1,
                                ProductSubCategoryId = 0,
                                ProductModelNumber = ModelNumber,
                                EntryDate = Convert.ToDateTime(SelectedDate),
                                CreationDate = Convert.ToDateTime(SelectedDate),
                                Quantity = (IsOutofStock == true ? (int)0 : (int)Quantity),
                                EntryByPersonId = (long)CommonAttribute.CustomeProfile?.PersonId,
                                ProductCategoryId = 0,
                                LocationId = SelectedStoreID,
                                StockEntryTypeId = InventoryFlag,
                                CountryId = CommonAttribute.CustomeProfile?.CountryId

                            });

                            TotalCount = obInventoryStockEntryRequest.Count.ToString();
                            addLocalInventoryEntryList();
                            Quantity = null; ModelNumber = "";
                        }
                        
                    }
                    catch (Exception ex)
                    { }
                });
            }
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
                    //if (Device.RuntimePlatform == Device.Android)
                    //{
                        //showToasterNoNetwork();
                    //}

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

        public void GetInventoryEntryList()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    #region inventory entry local db
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    //obInventoryStockEntryRequest.Clear();                    
                    var saleEntryLocalList = (List<TblInventoryStockEntryRequest>)inventoryDataEntryDb.GetInventoryEntrysBasedDateStore(SelectedStoreID, Convert.ToDateTime(SelectedDate),InventoryFlag);

                    if (saleEntryLocalList != null && saleEntryLocalList.Count > 0)
                    {
                        ObservableCollection<InventoryStockEntryRequest> inventoryStocks = new ObservableCollection<InventoryStockEntryRequest>();
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
                            inventoryStocks.Add(new InventoryStockEntryRequest
                            {
                                ProductModelId = ModelId,
                                ProductCategoryId = CategoryId,
                                ProductSubCategoryId = SubCategoryId,
                                ProductCategoryName = CategoryName,
                                ProductSubCategoryName = SubCategoryName,
                                ProductModelNumber = item.ProductModelNumber,
                                EntryDate = item.EntryDate,
                                CreationDate = item.CreationDate,
                                Quantity = item.Quantity,
                                EntryByPersonId = item.EntryByPersonId,                                
                                LocationId = item.LocationId,
                                StockEntryTypeId = item.StockEntryTypeId,
                                CountryId=item.CountryId,
                                InventoryEntrySubmittedStatus=(int)item.InventoryEntrySubmittedStatus,
                            });
                        }

                        obInventoryStockEntryRequest = inventoryStocks;
                        TotalCount = obInventoryStockEntryRequest.Count.ToString();

                    }
                    #endregion

                });
               
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public void addLocalInventoryEntryList()
        {
            try
            {
                #region sales entry local db
                int SelectedStoreID = 0;
                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                inventoryDataEntryDb.DeleteAllEntriesBasedDateStore(SelectedStoreID, Convert.ToDateTime(SelectedDate), InventoryFlag);

                if (obInventoryStockEntryRequest != null && obInventoryStockEntryRequest.Count > 0)
                {
                    foreach (var item in obInventoryStockEntryRequest)
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
                        tblInventoryStockEntryRequest  = new TblInventoryStockEntryRequest
                        {
                            ProductModelId = ModelId,
                            ProductCategoryId = CategoryId,
                            ProductSubCategoryId = SubCategoryId,
                            ProductCategoryName = CategoryName,
                            ProductSubCategoryName = SubCategoryName,
                            ProductModelNumber = item.ProductModelNumber,
                            EntryDate = item.EntryDate,
                            CreationDate = item.CreationDate,
                            Quantity = item.Quantity,
                            EntryByPersonId = item.EntryByPersonId,                            
                            LocationId = item.LocationId,
                            StockEntryTypeId = item.StockEntryTypeId,
                            CountryId=item.CountryId,
                            InventoryEntrySubmittedStatus=0,
                        };

                        inventoryDataEntryDb.AddInventoryEntry(tblInventoryStockEntryRequest);
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public async Task ValidateModelNumber()
        {
            if(NetworkAvailable)
            {
                MasterDataManagementSL dataManagementSL = new MasterDataManagementSL();
                if (!string.IsNullOrEmpty(ModelNumber) && ModelNumber.Length > 3)
                {
                    ModelNumberValide = false;

                    return;
                }
                long CountryID = (long)CommonAttribute.CustomeProfile?.CountryId;
                List<string> modelNumberSearchResponses = await dataManagementSL.SearchModelNumberByInitials(ModelNumber,CountryID);
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
            catch (Exception ex)
            {

            }
        }
            
        public Command ModelsPopupCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (NetworkAvailable)
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

        private ObservableCollection<DropDownModel> _StoreDropDown;
        public ObservableCollection<DropDownModel> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged("StoreDropDown");
            }
        }
        private DropDownModel _SelectedStore;
        public DropDownModel SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged("SelectedStore");
                GetInventoryEntryList();
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

        private ObservableCollection<InventoryStockEntryRequest> _obInventoryStockEntryRequest;
        public ObservableCollection<InventoryStockEntryRequest> obInventoryStockEntryRequest
        {
            get { return _obInventoryStockEntryRequest; }
            set
            {
                _obInventoryStockEntryRequest = value;
                OnPropertyChanged("obInventoryStockEntryRequest");
            }
        }

        private List<InventoryStockEntryRequest> _lstInventoryStockEntryRequest;
        public List<InventoryStockEntryRequest> lstInventoryStockEntryRequest
        {
            get { return _lstInventoryStockEntryRequest; }
            set
            {
                _lstInventoryStockEntryRequest = value;
                OnPropertyChanged("lstInventoryStockEntryRequest");
            }
        }

        private bool _IsOutofStock;
        public bool IsOutofStock
        {
            get { return _IsOutofStock; }
            set
            {
                _IsOutofStock = value;
                OnPropertyChanged("IsOutofStock");
            }
        }

        private bool _IsQtyListVisible;
        public bool IsQtyListVisible
        {
            get { return _IsQtyListVisible; }
            set
            {
                _IsQtyListVisible = value;
                OnPropertyChanged("IsQtyListVisible");
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

        private int _InventoryFlag;
        public int InventoryFlag

        {
            get { return _InventoryFlag; }
            set
            {
                _InventoryFlag = value;
                OnPropertyChanged("InventoryFlag");
            }
        }

        private string _SelectedDate;
        public string SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;

                OnPropertyChanged("SelectedDate");
                GetInventoryEntryList();
            }
        }

        private bool _EntryDateEnable;
        public bool EntryDateEnable
        {
            get { return _EntryDateEnable; }
            set
            {
                _EntryDateEnable = value;
                OnPropertyChanged(nameof(EntryDateEnable));
            }
        }

        private bool _IsEnableInventoryEntry;
        public bool IsEnableInventoryEntry
        {
            get { return _IsEnableInventoryEntry; }
            set
            {
                _IsEnableInventoryEntry = value;
                OnPropertyChanged(nameof(IsEnableInventoryEntry));
            }
        }
    }
}

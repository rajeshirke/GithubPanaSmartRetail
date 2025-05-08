using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Retail.Views.CommonPages;
using Retail.Views.PriceTracker;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Retail.Views.CommonPages.MultiselectPopupView;

namespace Retail.ViewModels.PriceTracker
{
    public class PriceTrackerViewModel : BaseViewModel
    {
        public PriceTrackerViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            IsPriceTrackerSaveEnable = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetLocation();
                
                SelectedDate = DateTime.Now.Date.ToString("dd,MMMM,yyyy");
                if (!(String.IsNullOrEmpty(CommonAttribute.PrvSelectedDate)))
                {
                    SelectedDate = CommonAttribute.PrvSelectedDate;
                }

                if (CommonAttribute.PrvSelectedStore != null)
                {
                    SelectedStore = CommonAttribute.PrvSelectedStore;
                    SelectedStoreText = SelectedStore.Title.ToString();
                }
                if (CommonAttribute.AllModelNumbers != null && CommonAttribute.AllModelNumbers.Count > 0)
                {
                    obModelNumbers = CommonAttribute.AllModelNumbers;
                }

                IsBusy = false;
            });
        }

        #region Methods

        public async Task GetModelNumbers()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }

                    PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                    obModelNumbers = new ObservableCollection<ProductModelForPriceDisplayResponse>();
                    var ModelNumbers = await priceDisplayTrackerManagement.SearchModelNumberByInitialForPriceDisplayTracker((int)CommonAttribute.CustomeProfile.PersonId, (int)CommonAttribute.CustomeProfile.CountryId, SelectedStoreID);

                    if (ModelNumbers != null && ModelNumbers.Count > 0)
                    {
                        CommonAttribute.AllModelNumbers = new ObservableCollection<ProductModelForPriceDisplayResponse>(ModelNumbers);
                        obModelNumbers = new ObservableCollection<ProductModelForPriceDisplayResponse>(ModelNumbers);
                        //obModelNumbers=new ObservableCollection<ProductModelForPriceDisplayResponse> 
                    }
                    IsBusy = false;
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

        public async Task GetLocation()
        {
            StoreDropDown = new ObservableCollection<DropDownModel>();

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await priceDisplayTrackerManagement.GetStoreForPriceDisplayTracker((int)CommonAttribute.CustomeProfile?.PersonId, (int)CommonAttribute.CustomeProfile?.CountryId);
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
                OfflineSearchModelNos();
            }

        }

        public void OfflineSearchModelNos()
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

        public async Task SavePriceTrackerEntry()
        {
            
            try
            {
                IsBusy = true;
              //  await Task.Delay(200);

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
                
                IsPriceTrackerSaveEnable = false;

                List<PriceTrackerResponse> LstpriceTrackerResponses = new List<PriceTrackerResponse>(ObPriceTrackerResponse);

                PriceTrackerEntryRequest priceTrackerEntryRequest = new PriceTrackerEntryRequest();

                PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();

                priceTrackerEntryRequest.Entrydate = Convert.ToDateTime(SelectedDate);
                priceTrackerEntryRequest.LocationId = SelectedStore.Id;
                priceTrackerEntryRequest.Panasonic_ModelNumber = ModelNumber;
                priceTrackerEntryRequest.PersonId = (long)(CommonAttribute.CustomeProfile?.PersonId);
                priceTrackerEntryRequest.ProductModelId = ModelID;
                priceTrackerEntryRequest.Comment = PriceTrackerComment;
                priceTrackerEntryRequest.PriceTrackerResponses = LstpriceTrackerResponses;

                var receiveContext = await priceDisplayTrackerManagement.PriceTrackerEntry(priceTrackerEntryRequest);
                if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                {
                    await Navigation.PushAsync(new FeedBackSuccessPage("Price Tracker"));
                }
                else if (receiveContext != null)
                {
                    await ErrorDisplayAlert(receiveContext.ErrorMessage);
                    IsPriceTrackerSaveEnable = true;
                }
                else
                {
                    await ErrorDisplayAlert(Resx.AppResources.lblErrorTitle);
                    IsPriceTrackerSaveEnable = true;
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
                IsPriceTrackerSaveEnable = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetCompModelsForPriceDisplayTracker(long SelectedModelID)
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedDate))
                {
                    await ErrorDisplayAlert("Please select date.");
                    return;
                }
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
                if (ModelID.ToString() == null || ModelID == 0)
                {
                    await ErrorDisplayAlert("Please select model number.");
                    return;
                }

                PriceTrackerListRequest priceTrackerListRequest = new PriceTrackerListRequest();

                long PersonId = (long)(CommonAttribute.CustomeProfile?.PersonId);

                priceTrackerListRequest.EntryDate = Convert.ToDateTime(SelectedDate);
                priceTrackerListRequest.ModelId = ModelID;
                priceTrackerListRequest.PersonId = PersonId;
                priceTrackerListRequest.StoreId = (int)(SelectedStore?.Id);

                PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                var PriceTrackerResponses = await priceDisplayTrackerManagement.GetCompetitorProductModelByModelIdForPriceDisplayTracker(priceTrackerListRequest);
                if (PriceTrackerResponses != null )
                {
                    if (PriceTrackerResponses.PriceTrackerResponse != null && PriceTrackerResponses.PriceTrackerResponse.Count > 0)
                    {
                        PriceTrackerResponses.PriceTrackerResponse.ToList().ForEach(p =>
                        {
                            //if (!(string.IsNullOrEmpty(p.RRP)) && p.RRP != "0" && p.NetRRP != "0" && !(string.IsNullOrEmpty(p.NetRRP)))
                            if (!(string.IsNullOrEmpty(p.RRP)) && !(string.IsNullOrEmpty(p.NetRRP)))
                            {
                                p.IsNonEditableFlg = true;
                            }
                        });
                        //PriceTrackerResponses.PriceTrackerResponse.ToList().ForEach(p =>
                        //{
                        //    if (ModelNumber == p.ProductModelName)
                        //    {
                        //        p.IsVisibleDeletePriceTrakcer = false;
                        //    }
                        //});
                        //for (int i = 1; i <= PriceTrackerResponses.PriceTrackerResponse.Count; i++)
                        //{
                        //    foreach (var item in PriceTrackerResponses.PriceTrackerResponse.ToList())
                        //    {
                        //        if (ModelNumber == item.ProductModelName)
                        //        {
                        //            item.IsVisibleDeletePriceTrakcer = false;
                        //        }
                        //    }
                        //}

                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>(PriceTrackerResponses.PriceTrackerResponse);
                    }
                    else
                    {
                        await ErrorDisplayAlert("No Records Found.");
                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>();

                        return;
                    }
                    if (PriceTrackerResponses.PriceTrackerResponseList != null && PriceTrackerResponses.PriceTrackerResponseList.Count > 0)
                    {
                        PriceTrackerResponseExistingList = new ObservableCollection<PriceTrackerResponseList>(PriceTrackerResponses.PriceTrackerResponseList);
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



        #endregion

        #region Commands

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
            //  ModelNumberValide=true
            ModelNumberValide = true;
            modelNumberSearchResponse = new ModelNumberSearchResponse() { ProductClassificationId = control.SelectedItem.Id, ModelName = control.SelectedItem.Title, ModelDescreption = control.SelectedItem.Desc };

        }

        public ICommand ConfirmPriceTrackerCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SavePriceTrackerEntry();
                });
            }
        }

        public Command SearchCompModelsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetCompModelsForPriceDisplayTracker(ModelID);
                });
            }
        }

        public Command DeletePriceTrackerCommand
        {
            get
            {
                return new Command<PriceTrackerResponse>((item) =>
                {
                    if (item != null)
                    {
                        ObPriceTrackerResponse.Remove(item);
                    }
                });
            }
        }

        public Command SelectStoreCommand
        {
            get
            {
                return new Command<DropDownModel>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedStore = obj;
                        CommonAttribute.PrvSelectedStore = obj;
                        MessagingCenter.Send<object, DropDownModel>(this, "SelectedStore", SelectedStore);
                        SelectedStoreText = SelectedStore.Title;
                        
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedStore.Id = 0;
                });
                
            }
        }

        public Command SearchStoreCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    StoreDropDown = new ObservableCollection<DropDownModel>(StoreDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        public Command SearchByCategoryCommand
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
                    await PopupNavigation.Instance.PushAsync(new PriceTrackerCategorySubcategoryPopup(SelectedStoreID));
                    ModelNumber = "";
                    CommonAttribute.PrvSelectedModelNo = new ProductModelForPriceDisplayResponse();

                });
            }
        }

        public ICommand CalculatePromoCommandRRP
        {
            get
            {
                return new Command<PriceTrackerResponse>((item) =>
                {
                    //if (!(string.IsNullOrEmpty(item.RRP)) && item.RRP != "0")
                    if (!(string.IsNullOrEmpty(item.RRP)))
                    {
                        if (item.NetRRP == "")
                        {
                            item.NetRRP = "0";
                        }
                        var CalculatedPromo = Convert.ToDouble(item.RRP) - Convert.ToDouble(item.NetRRP);
                        item.Promo = CalculatedPromo.ToString();
                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>(ObPriceTrackerResponse);
                    }
                    else
                    {
                        item.RRP = item.NetRRP = item.Promo = string.Empty;
                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>(ObPriceTrackerResponse);
                    }

                });
            }
        }

        public ICommand CalculatePromoCommand
        {
            get
            {
                return new Command<PriceTrackerResponse>((item) =>
                {
                    //if (!(string.IsNullOrEmpty(item.RRP)) && item.RRP != "0" && !(string.IsNullOrEmpty(item.NetRRP)) && item.NetRRP != "0" )
                    if (!(string.IsNullOrEmpty(item.RRP)) && !(string.IsNullOrEmpty(item.NetRRP)))
                    {
                        var CalculatedPromo = Convert.ToDouble(item.RRP) - Convert.ToDouble(item.NetRRP);
                        item.Promo = CalculatedPromo.ToString();
                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>(ObPriceTrackerResponse);
                    }
                    else
                    {
                        item.NetRRP = item.Promo = string.Empty;
                        ObPriceTrackerResponse = new ObservableCollection<PriceTrackerResponse>(ObPriceTrackerResponse);
                    }

                });
            }
        }
        #endregion

        #region Properties

        Connection c;

        public SalesDataEntryDb SalesDataEntryDb;

        public TmpSalesDataEntry TmpSalesData;

        public LocationStoreDb locationStoreDb;

        public ModelNumberSearchResponse modelNumberSearchResponse { get; set; }

        private string _SelectedStoreText;
        public string SelectedStoreText
        {
            get { return _SelectedStoreText; }
            set
            {
                _SelectedStoreText = value;
                OnPropertyChanged(nameof(SelectedStoreText));
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
            }
        }

        private ObservableCollection<ProductModelForPriceDisplayResponse> _obModelNumbers;
        public ObservableCollection<ProductModelForPriceDisplayResponse> obModelNumbers
        {
            get { return _obModelNumbers; }
            set
            {
                _obModelNumbers = value;
                OnPropertyChanged(nameof(obModelNumbers));
            }
        }

        private List<EntityKeyValueResponse> _Locations;
        public List<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged(nameof(Locations));
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

        private string _PriceTrackerComment;
        public string PriceTrackerComment
        {
            get { return _PriceTrackerComment; }
            set
            {                
                _PriceTrackerComment = value;
                OnPropertyChanged(nameof(PriceTrackerComment));
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
                OnPropertyChanged(nameof(ModelNumber));
            }
        }

        private long _ModelID;
        public long ModelID
        {
            get { return _ModelID; }
            set
            {
                ModelNumberValide = true;
                _ModelID = value;
                OnPropertyChanged(nameof(ModelID));
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
            }
        }

        private ObservableCollection<PriceTrackerResponseList> _PriceTrackerResponseExistingList;
        public ObservableCollection<PriceTrackerResponseList> PriceTrackerResponseExistingList
        {
            get { return _PriceTrackerResponseExistingList; }
            set
            {
                _PriceTrackerResponseExistingList = value;
                OnPropertyChanged(nameof(PriceTrackerResponseExistingList));
            }
        }

        private ObservableCollection<PriceTrackerResponse> _PriceTrackerResponse;
        public ObservableCollection<PriceTrackerResponse> ObPriceTrackerResponse
        {
            get { return _PriceTrackerResponse; }
            set
            {
                _PriceTrackerResponse = value;
                OnPropertyChanged(nameof(ObPriceTrackerResponse));
            }
        }

        private bool _IsPriceTrackerSaveEnable;
        public bool IsPriceTrackerSaveEnable
        {
            get { return _IsPriceTrackerSaveEnable; }
            set
            {
                _IsPriceTrackerSaveEnable = value;
                OnPropertyChanged(nameof(IsPriceTrackerSaveEnable));
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Hepler;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;
using static Retail.Views.CommonPages.MultiselectPopupView;

namespace Retail.ViewModels.PriceTracker
{
    public class PriceTrackerCategorySubcategoryViewModel : BaseViewModel
    {
        public PriceTrackerCategorySubcategoryViewModel(INavigation navigation,long SelectedStoreID) : base(navigation)
        {
            IsBusy = true;
            StoreId = SelectedStoreID;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                await GetCountries();
            });
        }
        public PriceTrackerCategorySubcategoryViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                await GetCountries();
            });
        }

        #region Methods

        public async Task GetCountries()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    AssignedCountries = new List<int>();
                    long? PersonID = CommonAttribute.CustomeProfile?.PersonId;
                    ////Whatever is assigned in web app only those will appear here
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> ActiveCountryList = await masterDataManagementSL.GetMultipleCountryIdByPersonId((long)PersonID);

                    if (ActiveCountryList != null && ActiveCountryList.Count > 0)
                    {
                        foreach(var item in ActiveCountryList)
                        {
                            AssignedCountries.Add(item.Id);
                        }
                    }
                }
                else
                {
                    //showToasterNoNetwork();
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

        public async Task GetCategoryForPriceDisplayTracker()
        {
            try
            {
                IsBusy = true;
                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);

                    CategoryDropDown = new ObservableCollection<DropDownModel>();
                    PriceDisplayTrackerManagement masterDataManagementSL = new PriceDisplayTrackerManagement();
                    List<EntityKeyValueResponse> entityKeyValueResponse = await masterDataManagementSL.GetCategoryForPriceDisplayTracker(PersonID, CountryID);
                    if (entityKeyValueResponse.Count != 0)
                    {
                        foreach (var item in entityKeyValueResponse)
                        {
                            CategoryDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        var list = CategoryDropDown.OrderBy(c => c.Title).ToList();
                        CategoryDropDown = new ObservableCollection<DropDownModel>(list);
                    }
                    else
                    {
                        IsBusy = false;
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
        }

        public Task GetSubCategoryForPriceDisplayTracker(long categoryId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);

                    var SubCategoryDropDownNew = new ObservableCollection<SubcategoryDropdownModel>();
                    SubCategoryDropDown = SubCategoryDropDownNew;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        ObservableCollection<SubcategoryDropdownModel> SubCategoryDropDown1 = new ObservableCollection<SubcategoryDropdownModel>();
                        PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                        List<EntityKeyValueResponse> EntityKeyValueResponses = await priceDisplayTrackerManagement.GetSubCategoryForPriceDisplayTracker(PersonID, categoryId, CountryID);
                        if (EntityKeyValueResponses.Count != 0)
                        {
                            foreach (var item in EntityKeyValueResponses)
                            {
                                SubCategoryDropDown1.Add(new SubcategoryDropdownModel { Id = item.Id, Title = item.Name });
                            }

                            var list = SubCategoryDropDown1.OrderBy(c => c.Title).ToList();
                            SubCategoryDropDown1 = new ObservableCollection<SubcategoryDropdownModel>(list);
                        }
                        else
                        {
                            IsBusy = false;
                            SubCategoryDropDown1 = new ObservableCollection<SubcategoryDropdownModel>();
                            SubCategoryDropDown = new ObservableCollection<SubcategoryDropdownModel>();
                            return;
                        }

                        if (SubCategoryDropDown1 != null)
                            SubCategoryDropDown = SubCategoryDropDown1;

                    });
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

            return Task.CompletedTask;
        }

        public async Task GetProductModelsForPriceDisplayTracker()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    
                    if (StoreId == 0)
                    {
                        await ErrorDisplayAlert("Please select store");
                        IsBusy = false;
                        await PopupNavigation.Instance.PopAsync();
                        return;
                    }

                    if (SelectedCategory == null && SelectedSubCategory == null)
                    {
                        await ErrorDisplayAlert("Please select category");
                        IsBusy = false;
                        return;
                    }

                    List<ProductModelForPriceDisplayResponse> productModelForPriceDisplayResponses = new List<ProductModelForPriceDisplayResponse>();

                    PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                    if (SelectedCategory != null && SelectedSubCategory != null)
                    {
                        productModelForPriceDisplayResponses = await priceDisplayTrackerManagement.GetProductModelForPriceDisplayTracker((long)(CommonAttribute.CustomeProfile?.PersonId), (long)(CommonAttribute.CustomeProfile?.CountryId), SelectedCategory.Id, SelectedSubCategory.Id, StoreId);
                    }
                    else if (SelectedCategory != null)
                    {
                        productModelForPriceDisplayResponses = await priceDisplayTrackerManagement.GetProductModelForPriceDisplayTracker((long)(CommonAttribute.CustomeProfile?.PersonId), (long)(CommonAttribute.CustomeProfile?.CountryId), SelectedCategory.Id, 0, StoreId);
                    }
                    if (productModelForPriceDisplayResponses != null && productModelForPriceDisplayResponses.Count != 0)
                    {
                        obProductModelForPriceDisplayResponses = new ObservableCollection<ProductModelForPriceDisplayResponse>(productModelForPriceDisplayResponses);
                    }
                    else
                    {
                        showToasterMessage("No data found");
                        IsBusy = false;
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
        }

        #endregion

        #region Commands

        public Command SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetProductModelsForPriceDisplayTracker();
                });
            }
        }
        
        public Command SearchCatgeoryCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    CategoryDropDown = new ObservableCollection<DropDownModel>(CategoryDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        public Command SearchSubcategoryCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    SubCategoryDropDown = new ObservableCollection<SubcategoryDropdownModel>(SubCategoryDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        public Command SelectSubCategoryCommand
        {
            get
            {
                return new Command<SubcategoryDropdownModel>(async (obj) =>
                {
                    SelectedSubCategory = new SubcategoryDropdownModel();
                    if (obj != null)
                    {
                        SelectedSubCategory = obj;
                        MessagingCenter.Send<object, SubcategoryDropdownModel>(this, "SelectedSubCategory", SelectedSubCategory);
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedSubCategory.Id = 0;
                });
            }
        }

        public Command SelectCategoryCommand
        {
            get
            {
                return new Command<DropDownModel>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedCategory = obj;
                        MessagingCenter.Send<object, DropDownModel>(this, "Selectedbcategory", SelectedCategory);
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedCategory.Id = 0;
                });
            }
        }

        public ICommand SelectModelNoCommand
        {

            get
            {
                return new Command<ProductModelForPriceDisplayResponse>(async (item) =>
                {
                    try
                    {
                        await PopupNavigation.Instance.PopAsync();
                        MessagingCenter.Send<object, ProductModelForPriceDisplayResponse>(this, "SelectedModelNoPriceTracker", item);
                        if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                            MessagingCenter.Send<object, ProductModelForPriceDisplayResponse>(this, "SelectedModelNoPriceTrackerAndroidiOS", item);

                    }
                    catch (Exception ex)
                    {
                        Debugger.Log(0, null, ex.ToString());
                    }
                });
            }
        }

        public Command ClearCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PopAsync();

                });
            }
        }

        public Command SearchModelByInitialCommand
        {
            get
            {
                return new Command((item) =>
                {
                    if (lstModelNo != null && lstModelNo.Count > 0)
                    {
                        lstModelNo = new ObservableCollection<ModelNoList>
                            (lstModelNo.Where(x => x.ModelNumber.ToLower().Contains(item.ToString().ToLower())).ToList());
                    }
                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Properties

        public List<int> AssignedCountries { get; set; }

        private long _StoreId;
        public long StoreId
        {
            get { return _StoreId; }
            set
            {
                _StoreId = value;
                OnPropertyChanged(nameof(StoreId));
            }
        }
        
        
        private string _SelectedCategoryText;
        public string SelectedCategoryText
        {
            get { return _SelectedCategoryText; }
            set
            {
                _SelectedCategoryText = value;
                OnPropertyChanged(nameof(SelectedCategoryText));
            }
        }

        private string _SelectedSubCategoryText;
        public string SelectedSubCategoryText
        {
            get { return _SelectedSubCategoryText; }
            set
            {
                _SelectedSubCategoryText = value;
                OnPropertyChanged(nameof(SelectedSubCategoryText));
            }
        }

        private bool _IsCatgeory;
        public bool IsCatgeory
        {
            get { return _IsCatgeory; }
            set
            {
                _IsCatgeory = value;
                OnPropertyChanged(nameof(IsCatgeory));
            }
        }

        private bool _IsSubCatgeory;
        public bool IsSubCatgeory
        {
            get { return _IsSubCatgeory; }
            set
            {
                _IsSubCatgeory = value;
                OnPropertyChanged(nameof(IsSubCatgeory));
            }
        }

        private ObservableCollection<ProductModelForPriceDisplayResponse> _obProductModelForPriceDisplayResponses;
        public ObservableCollection<ProductModelForPriceDisplayResponse> obProductModelForPriceDisplayResponses
        {
            get { return _obProductModelForPriceDisplayResponses; }
            set
            {
                _obProductModelForPriceDisplayResponses = value;
                OnPropertyChanged(nameof(obProductModelForPriceDisplayResponses));
            }
        }

        private ObservableCollection<ModelNoList> _lstModelNo;
        public ObservableCollection<ModelNoList> lstModelNo
        {
            get { return _lstModelNo; }
            set
            {
                _lstModelNo = value;
                OnPropertyChanged("lstModelNo");
            }
        }

        private ObservableCollection<SubcategoryDropdownModel> _SubCategoryDropDown;
        public ObservableCollection<SubcategoryDropdownModel> SubCategoryDropDown
        {
            get { return _SubCategoryDropDown; }
            set
            {
                _SubCategoryDropDown = value;
                OnPropertyChanged(nameof(SubCategoryDropDown));
                //SelectedSubCategory = new DropDownModel();                
            }
        }

        private SubcategoryDropdownModel _SelectedSubCategory;
        public SubcategoryDropdownModel SelectedSubCategory
        {
            get { return _SelectedSubCategory; }
            set
            {
                _SelectedSubCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        private ObservableCollection<DropDownModel> _CategoryDropDown;
        public ObservableCollection<DropDownModel> CategoryDropDown
        {
            get { return _CategoryDropDown; }
            set
            {
                _CategoryDropDown = value;
                OnPropertyChanged("CategoryDropDown");
            }
        }

        private DropDownModel _SelectedCategory;
        public DropDownModel SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        #endregion 
    }
}



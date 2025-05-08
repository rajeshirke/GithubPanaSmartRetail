using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Views.InventoryStock;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static Retail.Views.CommonPages.MultiselectPopupView;

namespace Retail.ViewModels.SalesTarget
{
    public class ProductCategoryPopupViewModel : BaseViewModel
    {
        public TmpSalesDataEntry TmpSalesData;
        public RecentlyModelStoreDb recentModelDb;
        Connection c = new Connection();

        public ProductCategoryPopupViewModel(INavigation navigation, int flag): base(navigation)
        {
            Flag = flag;

            //if (flag == 2)
            //{
            //    IsBusy = true;
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await GetRecentlyUsedModelNos();
            //        IsBusy = false;
            //    });
            //}
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            CategoryDropDown = new ObservableCollection<DropDownModel>();
            SubCategoryDropDown = new ObservableCollection<SubcategoryDropdownModel>();
            SelectedSubCategory = new SubcategoryDropdownModel();
            recentModelDb = new RecentlyModelStoreDb();

        }

        public async Task GetProductbyCategory()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    CategoryDropDown = new ObservableCollection<DropDownModel>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> entityKeyValueResponse = await masterDataManagementSL.GetProductMasterCategories();
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
                else
                {
                    await GetOfflineProductCategories();
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

        public async Task GetOfflineProductCategories()
        {
            try
            {
                CategoryDropDown = new ObservableCollection<DropDownModel>();
                var lstProductCategories = c.conn.Table<TblProductCategories>();
                if (lstProductCategories != null)
                {
                    foreach (var item in lstProductCategories)
                    {
                        CategoryDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                    }

                    var list = CategoryDropDown.OrderBy(c => c.Title).ToList();
                    CategoryDropDown = new ObservableCollection<DropDownModel>(list);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async Task GetSubcategory(long categoryId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    var SubCategoryDropDownNew = new ObservableCollection<SubcategoryDropdownModel>();
                    SubCategoryDropDown = SubCategoryDropDownNew;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        ObservableCollection<SubcategoryDropdownModel> SubCategoryDropDown1 = new ObservableCollection<SubcategoryDropdownModel>();
                        MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                        List<EntityKeyValueResponse> EntityKeyValueResponse = await masterDataManagementSL.GetProductSubCategoriesByCategoryId(categoryId);
                        if (EntityKeyValueResponse.Count != 0)
                        {
                            foreach (var item in EntityKeyValueResponse)
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
                else
                {
                    await GetOfflineSubcategories(categoryId);
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

        public async Task GetOfflineSubcategories(long categoryId)
        {
            try
            {
                var SubCategoryDropDownNew = new ObservableCollection<SubcategoryDropdownModel>();
                List<EntityKeyValueResponse> entityKeyValueResponse = new List<EntityKeyValueResponse>();
                SubCategoryDropDown = SubCategoryDropDownNew;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    List<ProductCategoryResponse> productCategoryResponse = new List<ProductCategoryResponse>();
                    ObservableCollection<SubcategoryDropdownModel> SubCategoryDropDown1 = new ObservableCollection<SubcategoryDropdownModel>();

                    var lstSubCategories = c.conn.Table<TblSubcategories>().ToList();
                    var lstAllSubcategories = lstSubCategories.Where(s => s.ParentCategoryId == categoryId).ToList();
                    if(lstAllSubcategories!=null && lstAllSubcategories.Count!=0)
                    {
                        foreach (var item in lstAllSubcategories)
                        {
                            entityKeyValueResponse.Add(new EntityKeyValueResponse { Id = item.ProductCategoryId, Name = item.Name });
                        }
                        if (entityKeyValueResponse!=null && entityKeyValueResponse.Count != 0)
                        {
                            foreach (var item in entityKeyValueResponse)
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
                    }

                });
            }
            catch(Exception ex)
            {

            }
        }

        public async Task GetRecentlyUsedModelNos()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                    List<string> lstModelNumbers = await salesTargetManagementSL.GetRecentTargetModelEntriesByPerson(CommonAttribute.CustomeProfile.PersonId);
                    if (lstModelNumbers.Count != 0)
                    {
                        lstModelNo = new ObservableCollection<ModelNoList>();
                        foreach (string modelno in lstModelNumbers)
                        {
                            lstModelNo.Add(new ModelNoList { ModelNumber = modelno });
                        }
                    }
                    else
                    {
                        //await ErrorDisplayAlert("No data found");
                        showToasterMessage("No data found");
                        IsBusy = false;
                        return;
                    }
                }
                else
                {
                    await GetOfflineRecentlyUsedModelNos();
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

        public async Task GetOfflineRecentlyUsedModelNos()
        {
            try
            {
                var tblRecentModels = (List<string>)recentModelDb.GetRecentlyModels().ToList();
                if (tblRecentModels != null && tblRecentModels.Count() > 0)
                {
                    lstModelNo = new ObservableCollection<ModelNoList>();
                    foreach (string modelno in tblRecentModels)
                    {
                        lstModelNo.Add(new ModelNoList { ModelNumber = modelno });
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task GetOnlineModelNosByCategory()
        {
            if (SelectedCategory == null && SelectedSubCategory == null)
            {
                await ErrorDisplayAlert("Please select category");
                IsBusy = false;
                return;
            }
            List<string> lstModelNumbers = new List<string>();
            lstModelNo = new ObservableCollection<ModelNoList>();
            if (lstModelNumbers != null)
            {
                lstModelNumbers = new List<string>();
            }
            SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
            //if (SelectedCategory != null && SelectedSubCategory.Id == 0)
            //{
            //    lstModelNumbers = await salesTargetManagementSL.GetOnlyProductModelNumbersActiveByCategoryId(CommonAttribute.CustomeProfile.CountryId, SelectedCategory.Id);
            //}
            if (SelectedCategory != null && SelectedSubCategory != null)
            {
                lstModelNumbers = await salesTargetManagementSL.GetOnlyProductModelNumbersActiveByCategoryId(CommonAttribute.CustomeProfile.CountryId, SelectedCategory.Id, SelectedSubCategory.Id);
            }
            else if (SelectedCategory != null)
            {
                lstModelNumbers = await salesTargetManagementSL.GetOnlyProductModelNumbersActiveByCategoryId(CommonAttribute.CustomeProfile.CountryId, SelectedCategory.Id, 0);
            }
            if (lstModelNumbers != null && lstModelNumbers.Count != 0)
            {
                lstModelNo = new ObservableCollection<ModelNoList>();
                foreach (string modelno in lstModelNumbers)
                {
                    lstModelNo.Add(new ModelNoList { ModelNumber = modelno });
                }
            }
            else
            {
                //await ErrorDisplayAlert("No data found");
                showToasterMessage("No data found");
                IsBusy = false;
                return;
            }
        }

        public async Task GetModelNosByCategory()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //online data
                    await GetOnlineModelNosByCategory();
                }
                else
                {
                    //Offline Data
                    await GetOfflineModelNumberByCategory();
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

        public async Task GetOfflineModelNumberByCategory()
        {
            List<string> lstModelNumbers = new List<string>();
            lstModelNo = new ObservableCollection<ModelNoList>();
            if (lstModelNumbers != null)
            {
                lstModelNumbers = new List<string>();
            }

            var lstModelNosByCategory = c.conn.Table<TblAllModelNumbers>().ToList();
            if (SelectedCategory != null && SelectedSubCategory.Id != 0)
            {
                lstModelNumbers = lstModelNosByCategory.Where(c => c.ProductCategoryId == SelectedCategory.Id && c.ProductSubCategoryId == SelectedSubCategory.Id).Select(m => m.ProductModelNumber).ToList();
            }
            else if(SelectedCategory != null)
            {
                lstModelNumbers = lstModelNosByCategory.Where(c => c.ProductCategoryId == SelectedCategory.Id).Select(m => m.ProductModelNumber).ToList();

            }

            if (lstModelNumbers != null && lstModelNumbers.Count != 0)
            {
                lstModelNo = new ObservableCollection<ModelNoList>();
                foreach (string modelno in lstModelNumbers)
                {
                    lstModelNo.Add(new ModelNoList { ModelNumber = modelno });
                }
            }
            else
            {
                //await ErrorDisplayAlert("No data found");
                showToasterMessage("No data found");
                IsBusy = false;
                return;
            }

        }

        public Command SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetModelNosByCategory();

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


        public ICommand SelectModelNoCommand
        {
           
            get
            {
                return new Command<ModelNoList>(async (item) =>
                {
                    try
                    {
                        if (Flag == 2)
                        {
                            MessagingCenter.Send<object, string>(this, "SelectedModelNo", item.ModelNumber);
                            await PopupNavigation.Instance.PopAsync();
                        }
                        if (Flag == 1)
                        {
                            MessagingCenter.Send<object, string>(this, "SelectedModelNo", item.ModelNumber);
                            await PopupNavigation.Instance.PopAsync();
                        }

                    }
                    catch (Exception ex)
                    {
                        Debugger.Log(0, null, ex.ToString());
                    }
                });
            }
        }

        public Command SelectSubCategoryCommand
        {
            get
            {
                return new Command<SubcategoryDropdownModel>((obj) =>
                {
                    SelectedSubCategory = new SubcategoryDropdownModel();
                    if (obj != null)
                        SelectedSubCategory = obj;
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
                        await GetSubcategory((long)SelectedCategory.Id);
                    }
                    else
                        SelectedCategory.Id = 0;

                });
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


        private ObservableCollection<EntityKeyValueResponse> _lstEntityKeyValueResponse;
        public ObservableCollection<EntityKeyValueResponse> lstEntityKeyValueResponse
        {
            get { return _lstEntityKeyValueResponse; }
            set
            {
                _lstEntityKeyValueResponse = value;
                OnPropertyChanged("lstEntityKeyValueResponse");
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

        private string _PriceDisplayTracker;
        public string PriceDisplayTracker

        {
            get { return _PriceDisplayTracker; }
            set
            {
                _PriceDisplayTracker = value;
                OnPropertyChanged(nameof(PriceDisplayTracker));
            }
        }

        private int _Flag;
        public int Flag

        {
            get { return _Flag; }
            set
            {
                _Flag = value;
                OnPropertyChanged("Flag");
            }
        }
    } 
}


public class ProductCategory
{
    public string ProductCategoryName { get; set; }
}

public class ModelNoList
{
    public string ModelNumber { get; set; }
}
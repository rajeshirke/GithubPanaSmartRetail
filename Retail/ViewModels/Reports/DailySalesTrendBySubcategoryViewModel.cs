using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Resx;
using Retail.Views.Reports.Charts;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class DailySalesTrendBySubcategoryViewModel : BaseViewModel
    {
        public bool IsExpanded { get; set; }
        public DailySalesTrendBySubcategoryViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsExpanded = true;
                SelectedDate = DateTime.Now.ToString("yyyy-MM-dd");
                SelectedAccount = new AccountsDropdown();
                AccountDropDown = new ObservableCollection<AccountsDropdown>();
                obSalesEntryDailyBySubCategoryView = new ObservableCollection<SalesEntryDailyBySubCategoryView>();
                await GetSubcategory();
                IsBusy = false;
            });
        }

        public async Task GetSubcategory()
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
                        List<ProductCategoryResponse> productCategoryResponse = await masterDataManagementSL.GetAllSubcategoriesforOffline();
                        if (productCategoryResponse.Count != 0)
                        {
                            foreach (var item in productCategoryResponse)
                            {
                                SubCategoryDropDown1.Add(new SubcategoryDropdownModel { Id = item.ProductCategoryId, Title = item.Name });
                            }
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
        }

        public async Task GetAccounts()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    AccountDropDown = new ObservableCollection<AccountsDropdown>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> Accounts = await masterDataManagementSL.GetAccountsByCountryId(CommonAttribute.CustomeProfile.CountryId);
                    if (Accounts.Count != 0 && Accounts != null)
                    {
                        foreach (var item in Accounts)
                        {
                            AccountDropDown.Add(new AccountsDropdown { Id = item.Id, Title = item.Name });
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

        public async Task GetDailySalesTrendBySubcategoryReports(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if (reportCreateUpdateRequest != null)
                    {
                        obSalesEntryDailyBySubCategoryView = new ObservableCollection<SalesEntryDailyBySubCategoryView>();
                        ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                        List<SalesEntryDailyBySubCategoryView> salesEntryDailyBySubCategoryView = await reportsManagementSL.GetDailySalesTrendBySubcategoryAll(reportCreateUpdateRequest);
                        if (salesEntryDailyBySubCategoryView != null && salesEntryDailyBySubCategoryView.Count != 0)
                        {
                            //obSalesEntryDailyBySubCategoryView = new ObservableCollection<SalesEntryDailyBySubCategoryView>(salesEntryDailyBySubCategoryView.Where(s => s.ProductSubCategoryName != null && s.ProductSubCategoryName != string.Empty).OrderByDescending(d => d.EntryDate).ToList());
                            obSalesEntryDailyBySubCategoryView = new ObservableCollection<SalesEntryDailyBySubCategoryView>(salesEntryDailyBySubCategoryView.OrderBy(d => d.EntryDate).ToList());
                        }
                        else
                        {
                            obSalesEntryDailyBySubCategoryView = new ObservableCollection<SalesEntryDailyBySubCategoryView>();
                        }
                    }          
                }
                else
                {
                    
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

        public Command ChartClick
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new DailySalesTrendBySubcategoryChart(obSalesEntryDailyBySubCategoryView));
                });
            }
        }

        public Command SelectAccountCommand
        {
            get
            {
                return new Command<AccountsDropdown>((obj) =>
                {
                    if (obj != null)
                        SelectedAccount = obj;
                    else
                        SelectedAccount.Id = 0;
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


        private ObservableCollection<SubcategoryDropdownModel> _SubCategoryDropDown;
        public ObservableCollection<SubcategoryDropdownModel> SubCategoryDropDown
        {
            get { return _SubCategoryDropDown; }
            set
            {
                _SubCategoryDropDown = value;
                OnPropertyChanged(nameof(SubCategoryDropDown));
            }
        }

        private SubcategoryDropdownModel _SelectedSubCategory;
        public SubcategoryDropdownModel SelectedSubCategory
        {
            get { return _SelectedSubCategory; }
            set
            {
                _SelectedSubCategory = value;
                OnPropertyChanged(nameof(SelectedSubCategory));
            }
        }

        private ObservableCollection<SalesEntryDailyBySubCategoryView> _obSalesEntryDailyBySubCategoryView;
        public ObservableCollection<SalesEntryDailyBySubCategoryView> obSalesEntryDailyBySubCategoryView
        {
            get { return _obSalesEntryDailyBySubCategoryView; }
            set
            {
                _obSalesEntryDailyBySubCategoryView = value;
                OnPropertyChanged(nameof(obSalesEntryDailyBySubCategoryView));
            }
        }

        private ObservableCollection<AccountsDropdown> _AccountDropDown;
        public ObservableCollection<AccountsDropdown> AccountDropDown
        {
            get { return _AccountDropDown; }
            set
            {
                _AccountDropDown = value;
                OnPropertyChanged(nameof(AccountDropDown));
            }
        }

        private AccountsDropdown _SelectedAccount;
        public AccountsDropdown SelectedAccount
        {
            get { return _SelectedAccount; }
            set
            {
                _SelectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));

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
    }
}


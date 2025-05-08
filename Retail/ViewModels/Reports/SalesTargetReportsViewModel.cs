using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Resx;
using Retail.Views.Reports.Charts;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class SalesTargetReportsViewModel:BaseViewModel
    {
        public bool IsExpanded { get; set; }
        public SalesTargetReportsViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsExpanded = true;
                CurrentDate = DateTime.Now.ToString("MMMM,yyyy");
                SelectedAccount = new AccountsDropdown();                
                CityDropDown = new ObservableCollection<DropDownModel>();
                AccountDropDown = new ObservableCollection<AccountsDropdown>();
                //await GetCities();
                //await GetAccounts();
                IsBusy = false;
            });
        }

        public async Task GetCities()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    CityDropDown = new ObservableCollection<DropDownModel>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> Cities = await masterDataManagementSL.GetCitiesByCountryId(CommonAttribute.CustomeProfile.CountryId);
                    if (Cities.Count != 0 && Cities != null)
                    {
                        foreach (var item in Cities)
                        {
                            CityDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
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

        public async Task GetAccounts()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    AccountDropDown = new ObservableCollection<AccountsDropdown>();
                    SelectedAccount = new AccountsDropdown();
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

        public async Task GetSalesAchievedvsCountryReports(ReportCreateUpdateRequest reportCreateUpdate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //if (SelectedCity != null && SelectedAccount != null)
                    //{
                    //    if (SelectedAccount.Id != 0 && SelectedCity.Id != 0)
                    //    {
                        if (reportCreateUpdate != null)
                        {
                            ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                            List<SalesTargetCountryCityAccountReportView> salesTargetCountryCityAccountReportView = await reportsManagementSL.GetSalesAchievedvsCountry(reportCreateUpdate);
                            if (salesTargetCountryCityAccountReportView != null && salesTargetCountryCityAccountReportView.Count != 0)
                            {
                                var list = salesTargetCountryCityAccountReportView.OrderBy(c => c.City).ToList();
                                obSalesTargetCountryCityAccountReportView = new ObservableCollection<SalesTargetCountryCityAccountReportView>(list);
                            }
                            else
                            {
                                obSalesTargetCountryCityAccountReportView = new ObservableCollection<SalesTargetCountryCityAccountReportView>();
                            }
                        }
                        else
                        {
                            obSalesTargetCountryCityAccountReportView = new ObservableCollection<SalesTargetCountryCityAccountReportView>();
                        }

                           
                    //    }
                    //    else
                    //    {
                    //        await ErrorDisplayAlert("Please select account.");
                    //    }
                    //}
                    //else
                    //{
                    //    await ErrorDisplayAlert("Please select city and account.");
                    //}

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

        public async Task GetSalesAchievedvsCategoryReports(ReportCreateUpdateRequest reportCreateUpdate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //if (SelectedCity != null && SelectedAccount != null)
                    //{
                    //    if (SelectedAccount.Id != 0 && SelectedCity.Id != 0)
                    //    {
                    if (reportCreateUpdate != null)
                    {
                        ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                        List<SalesTargetProductCategoryReportView> salesTargetProductCategoryReportView = await reportsManagementSL.GetSalesAchievedvsProductCategory(reportCreateUpdate);
                        if (salesTargetProductCategoryReportView != null && salesTargetProductCategoryReportView.Count != 0)
                        {
                            var list = salesTargetProductCategoryReportView.OrderBy(c => c.City).ToList();
                            obSalesTargetProductCategoryReportView = new ObservableCollection<SalesTargetProductCategoryReportView>(list);
                        }
                        else
                        {
                            obSalesTargetProductCategoryReportView = new ObservableCollection<SalesTargetProductCategoryReportView>();
                        }
                    }
                    else
                    {
                        obSalesTargetProductCategoryReportView = new ObservableCollection<SalesTargetProductCategoryReportView>();
                    }

                    //    }
                    //    else
                    //    {
                    //        await ErrorDisplayAlert("Please select account.");
                    //    }
                    //}
                    //else
                    //{
                    //    await ErrorDisplayAlert("Please select city and account");
                    //}

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

        private ObservableCollection<SalesTargetProductCategoryReportView> _obSalesTargetProductCategoryReportView;
        public ObservableCollection<SalesTargetProductCategoryReportView> obSalesTargetProductCategoryReportView
        {
            get { return _obSalesTargetProductCategoryReportView; }
            set
            {
                _obSalesTargetProductCategoryReportView = value;
                OnPropertyChanged(nameof(obSalesTargetProductCategoryReportView));
            }
        }

        public Command ChartClick
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsCategoryChecked)
                    {
                        await PopupNavigation.Instance.PushAsync(new SalesAchievedvsProductCat(obSalesTargetProductCategoryReportView));
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new SalesAchievedvsCountry(obSalesTargetCountryCityAccountReportView));
                    }
                });
            }
        }

        //public Command ShowReportsCommand
        //{
        //    get
        //    {
        //        return new Command(async() =>
        //        {
        //            if (SelectedCity == null)
        //            {
        //                await ErrorDisplayAlert("Please select city.");
        //                return;
        //            }
        //            if (SelectedAccount == null)
        //            {
        //                await ErrorDisplayAlert("Please select account.");
        //                return;
        //            }
        //            try
        //            {
        //                await GetSalesAchievedvsCountryReports();
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        });
        //    }
        //}

        //public Command ShowSalesCategoryReportsCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            if (SelectedCity == null)
        //            {
        //                await ErrorDisplayAlert("Please select city.");
        //                return;
        //            }
        //            if (SelectedAccount == null )
        //            {
        //                await ErrorDisplayAlert("Please select account.");
        //                return;
        //            }
        //            try
        //            {
        //                await GetSalesAchievedvsCategoryReports();
        //            }
        //            catch (Exception ex)
        //            {

        //            }
                    
        //        });
        //    }
        //}

        public Command SelectCityCommand
        {
            get
            {
                return new Command<DropDownModel>((obj) =>
                {
                    if (obj != null)
                        SelectedCity = obj;
                    else
                        SelectedCity.Id = 0;
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

        private ObservableCollection<SalesTargetCountryCityAccountReportView> _obSalesTargetCountryCityAccountReportView;
        public ObservableCollection<SalesTargetCountryCityAccountReportView> obSalesTargetCountryCityAccountReportView
        {
            get { return _obSalesTargetCountryCityAccountReportView; }
            set
            {
                _obSalesTargetCountryCityAccountReportView = value;
                OnPropertyChanged(nameof(obSalesTargetCountryCityAccountReportView));
            }
        }

        private ObservableCollection<DropDownModel> _CityDropDown;
        public ObservableCollection<DropDownModel> CityDropDown
        {
            get { return _CityDropDown; }
            set
            {
                _CityDropDown = value;
                OnPropertyChanged(nameof(CityDropDown));
            }
        }

        private DropDownModel _SelectedCity;
        public DropDownModel SelectedCity
        {
            get { return _SelectedCity; }
            set
            {
               
                _SelectedCity = value;              

                OnPropertyChanged(nameof(SelectedCity));
               
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

        private string _CurrentDate;
        public string CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                _CurrentDate = value;
                OnPropertyChanged(nameof(CurrentDate));               
            }
        }

        private bool _IsCategoryChecked;
        public bool IsCategoryChecked
        {
            get { return _IsCategoryChecked; }
            set
            {
                _IsCategoryChecked = value;
                OnPropertyChanged(nameof(IsCategoryChecked));
            }
        }
    }
}

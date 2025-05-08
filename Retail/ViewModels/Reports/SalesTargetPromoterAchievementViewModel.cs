using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
using Xamarin.Forms.Internals;

namespace Retail.ViewModels.Reports
{
    public class SalesTargetPromoterAchievementViewModel : BaseViewModel
    {
        public bool IsExpanded { get; set; }
        public SalesTargetPromoterAchievementViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsExpanded = true;
                CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                SelectedAccount = new AccountsDropdown();                
                AccountDropDown = new ObservableCollection<AccountsDropdown>();
                //await GetAccounts();
                //await GetLocation();
                IsBusy = false;
            });
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
                    List<EntityKeyValueResponse> Accounts = await masterDataManagementSL.GetAccountsByCountryId((int)CommonAttribute.CustomeProfile?.CountryId);
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

        public async Task GetLocation()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    StoreDropDown = new ObservableCollection<DropDownModel>();

                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await masterDataManagementSL.GetLocationsByCountryId((int)CommonAttribute.CustomeProfile.CountryId);
                    if (Locations.Count != 0)
                    {
                        foreach (var item in Locations)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }
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

        public async Task GetSalesTargetPromoterAchievementReport(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //if (SelectedAccount != null && SelectedStore != null)
                    //{
                    //    if (SelectedStore.Id != 0 && SelectedAccount.Id != 0)
                    //    {
                    ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                    List<SalesTargetPromoterAchievementReportView> salesTargetPromoterAchievementReportView = await reportsManagementSL.GetSalesTargetPromoterAchievementReportByCountryAccountLocationMultiple(reportCreateUpdateRequest);
                    if (salesTargetPromoterAchievementReportView != null && salesTargetPromoterAchievementReportView.Count != 0)
                    {
                        var list = salesTargetPromoterAchievementReportView.OrderBy(nm => nm.PersonName).ToList();
                        obSalesTargetPromoterAchievementReportView = new ObservableCollection<SalesTargetPromoterAchievementReportView>(list);

                        //foreach (var item in salesTargetPromoterAchievementReportView)
                        //{
                        //    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)item.TargetMonth);
                        //    obSalesTargetPromoterAchievementReportView.Where(m => m.TargetMonthNm = monthName);
                            
                        //}
                                
                    }
                    //    }
                    //    else
                    //    {
                    //        await ErrorDisplayAlert("Please select account.");
                    //    }
                    //}
                    //else
                    //{
                    //    await ErrorDisplayAlert("Please select store and account.");
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

        public Command ChartClick
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new PromoterAchievedPercentage(obSalesTargetPromoterAchievementReportView));
                });
            }
        }

        public Command ShowReportsCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (SelectedAccount == null || SelectedAccount.Id == 0)
                    {
                        await ErrorDisplayAlert("Please select account.");
                        return;
                    }
                    if (SelectedStore == null)
                    {
                        await ErrorDisplayAlert("Please select store.");
                        return;
                    }
                    try
                    {
                        //await GetSalesTargetPromoterAchievementReport();
                    }
                    catch (Exception ex)
                    {

                    }
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

        private ObservableCollection<SalesTargetPromoterAchievementReportView> _obSalesTargetPromoterAchievementReportView;
        public ObservableCollection<SalesTargetPromoterAchievementReportView> obSalesTargetPromoterAchievementReportView
        {
            get { return _obSalesTargetPromoterAchievementReportView; }
            set
            {
                _obSalesTargetPromoterAchievementReportView = value;
                OnPropertyChanged(nameof(obSalesTargetPromoterAchievementReportView));
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
    }
}

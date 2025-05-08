using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Resx;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class VisitTargetsReportViewModel : BaseViewModel
    {
        public bool IsExpanded { get; set; }
        public VisitTargetsReportViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsExpanded = true;                            
                SupervisorDropDown = new ObservableCollection<DropDownModel>();
                AccountDropDown = new ObservableCollection<AccountsDropdown>();
                
                List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();
                if (lstRoleIds != null && lstRoleIds.Count > 0)
                {
                    if(lstRoleIds.Contains((int)PersonRoleEnum.Supervisor))
                    {
                        IsSupervisorFilterVisible = false;
                    }
                    else
                    {

                    }
                }                             
                IsBusy = false;
            });
        }

        public async Task GetSupervisors(int SelectedAccountId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if (SelectedAccount != null && SelectedAccount.Id != 0)
                    {
                        SupervisorDropDown = new ObservableCollection<DropDownModel>();
                        MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                        List<EntityKeyValueResponse> Supervisors = await masterDataManagementSL.GetSupervisorsByAccountId(SelectedAccountId);
                        if (Supervisors.Count != 0 && Supervisors != null)
                        {
                            foreach (var item in Supervisors)
                            {
                                SupervisorDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                            }
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

        public async Task GetDailyVisitTargetTasksReports(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    //if (SelectedAccount != null && SelectedSupervisor != null)
                    //{
                    //    if (SelectedAccount.Id != 0 && SelectedSupervisor.Id != 0)
                    //    {
                            ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                            List<DailyVisitTargetTasksReportView> dailyVisitTargetTasksReportView = await reportsManagementSL.GetDailyVisitTargetTasksReportAll(reportCreateUpdateRequest);
                            if (dailyVisitTargetTasksReportView != null && dailyVisitTargetTasksReportView.Count != 0)
                            {
                                StoreVisitDetailsHt = dailyVisitTargetTasksReportView.Count * 10;

                                var list = dailyVisitTargetTasksReportView.OrderBy(c => c.PersonName).ToList();
                                obDailyVisitTargetTasksReportView = new ObservableCollection<DailyVisitTargetTasksReportView>(list.OrderByDescending(d => d.VisitScheduleDate));                                
                            }
                            else
                            {
                                obDailyVisitTargetTasksReportView = new ObservableCollection<DailyVisitTargetTasksReportView>();
                            }
                    //    }
                    //    else
                    //    {
                    //        await ErrorDisplayAlert("Please select supervisor.");
                    //    }
                    //}
                    //else
                    //{
                    //    await ErrorDisplayAlert("Please select account.");
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

        public Command SelectAccountCommand
        {
            get
            {
                return new Command<AccountsDropdown>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedAccount = obj;
                        await GetSupervisors(SelectedAccount.Id);
                    }
                    else
                        SelectedAccount.Id = 0;
                });
            }
        }

        public Command ShowReportsCommand
        {
            get
            {
                return new Command(async () =>
                {
                   
                    if (SelectedAccount == null)
                    {
                        await ErrorDisplayAlert("Please select account.");
                        return;
                    }
                    if (SelectedSupervisor == null)
                    {
                        await ErrorDisplayAlert("Please select supervisor.");
                        return;
                    }
                    try
                    {
                        //await GetDailyVisitTargetTasksReports();
                        
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }


        private ObservableCollection<DailyVisitTargetTasksReportView> _obDailyVisitTargetTasksReportView;
        public ObservableCollection<DailyVisitTargetTasksReportView> obDailyVisitTargetTasksReportView
        {
            get { return _obDailyVisitTargetTasksReportView; }
            set
            {
                _obDailyVisitTargetTasksReportView = value;
                OnPropertyChanged(nameof(obDailyVisitTargetTasksReportView));
            }
        }

        private ObservableCollection<DropDownModel> _SupervisorDropDown;
        public ObservableCollection<DropDownModel> SupervisorDropDown
        {
            get { return _SupervisorDropDown; }
            set
            {
                _SupervisorDropDown = value;
                OnPropertyChanged(nameof(SupervisorDropDown));
            }
        }

        private DropDownModel _SelectedSupervisor;
        public DropDownModel SelectedSupervisor
        {
            get { return _SelectedSupervisor; }
            set
            {
                _SelectedSupervisor = value;
                OnPropertyChanged(nameof(SelectedSupervisor));

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

        private bool _IsSupervisorFilterVisible = false;
        public bool IsSupervisorFilterVisible
        {
            get { return _IsSupervisorFilterVisible; }
            set
            {
                _IsSupervisorFilterVisible = value;
                OnPropertyChanged(nameof(IsSupervisorFilterVisible));
            }
        }

        private int _StoreVisitDetailsHt;
        public int StoreVisitDetailsHt
        {
            get
            {
                return _StoreVisitDetailsHt;
            }
            set
            {
                _StoreVisitDetailsHt = value;
                OnPropertyChanged(nameof(StoreVisitDetailsHt));
            }
        }
    }
}


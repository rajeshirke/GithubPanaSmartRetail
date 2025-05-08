using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Retail.Views.Attendance;
using Retail.Views.Dashboard;
 
using Retail.Views.InventoryStock;
using Retail.Views.MarketIntelligence;
using Retail.Views.MyVisits;
using Retail.Views.PriceTracker;
using Retail.Views.ProductCatalogue;
using Retail.Views.Reports;
using Retail.Views.SalesTargetViews;
using Retail.Views.SupervisorFlow;
using Xamarin.Forms;

namespace Retail.ViewModels.Dashboard
{
    public class DashboardMenuPageViewModel : BaseViewModel
    {

        public DashboardMenuPageViewModel(INavigation navigation) : base(navigation)
        {

            List<MobileMainMenuResponse> DBMobileAppModules = (List<MobileMainMenuResponse>)CommonAttribute.CustomeProfile.MobileMainMenus;
            if (DBMobileAppModules != null && DBMobileAppModules.Count > 0)
            {
                var DBMobileAppModulesAsc = DBMobileAppModules.OrderBy(p => p.MenuOrder).ToList();               

                foreach (var MobileAppModule in DBMobileAppModulesAsc)
                {
                    switch (MobileAppModule.Name)
                    {
                        case "Visits & Tasks":       //for promoter visits and tasks
                            IsVisitsandTasks = true;
                            break;

                        case "Sales Target":
                            IsSalesTarget = true;
                            break;

                        case "Sales Data Entry":
                            IsSalesEntry = true;
                            break;

                        case "Sales Return":
                            IsSalesReturn = true;
                            break;

                        case "Attendance":
                            IsAttendance = true;
                            break;

                        case "Inventory Stock":
                            IsInventory = true;
                            break;

                        case "Product Catalogue":
                            IsProductCatalogue = true;
                            break;

                        case "Reports":
                            IsReports = true;
                            break;

                        case "Market Insights":
                            IsMarketInsights = true;
                            break;

                        case "Targets Overview":
                            IsTargetsOverview = true;
                            break;

                        case "Attendance Tracking":
                            IsAttendanceTracker = true;
                            break;

                        case "Out of Stock Entry":
                            IsOutOfStock = true;
                            break;

                        case "Supervisor Visits":        //for supervisor visits and tasks
                            IsSupervisorVisits = true;
                            break;

                        case "Price Tracker Entry":
                            IsPriceTracker = true;
                            break;

                        case "Display Tracker Entry":
                            IsDisplayTracker = true;
                            break;

                    }

                }

                //for dashboard bottom tabs 
                var BottomTabMenues = DBMobileAppModules.OrderBy(p => p.MenuOrder).
                   Where(m => m.Name == "Visits & Tasks" || m.Name == "Sales Data Entry" ||
                   m.Name == "Supervisor Visits" || m.Name == "Targets Overview" || m.Name == "Reports").ToList();

                if (BottomTabMenues != null && BottomTabMenues.Count > 0)
                {
                    var SelectedBottomTab = BottomTabMenues.Take(3).ToList();

                    foreach (var MobileAppModule in SelectedBottomTab)
                    {
                        switch (MobileAppModule.Name)
                        {
                            case "Visits & Tasks":       //for promoter visits and tasks
                                IsVisitsandTasksBottomTab = true;
                                break;


                            case "Sales Data Entry":
                                IsSalesEntryBottomTab = true;
                                break;


                            case "Reports":
                                IsReportsBottomTab = true;
                                break;

                            case "Targets Overview":
                                IsTargetsOverviewBottomTab = true;
                                break;


                            case "Supervisor Visits":
                                IsSupervisorVisitsBottomTab = true;
                                break;

                        }
                    }
                }
                
            }

            //TabVisibleCheck();

        }

        private void TabVisibleCheck()
        {
            List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();
            if (!(lstRoleIds.Contains((int)PersonRoleEnum.Promoter)) || !(lstRoleIds.Contains((int)PersonRoleEnum.FieldServiceAgent)))
            {
                IsVisitsandTasks = false; 
            }           
        }

        public bool PromoterTabVisible { get; set; }
        public bool SupervisorTabVisible { get; set; }        

        public Command SalesEntryCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new SalesDataEntry());
                });
            }
        }

        public Command HomeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new DashboardMasterPage());
                });
            }
        }

        public Command MarketInsightsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new MarketIntelligenceView());
                });
            }
        }

        public Command VisitsandTasksCommand
        {
            get
            {
                return new Command(() =>
                {
                    RunAllJobs();
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new PlannedVisitsView());
                });
            }
        }



        private async Task RunAllJobs()
        {
            //var access = await ShinyHost.Resolve<INotificationManager>().RequestAccess();
            //var result = await ShinyHost.Resolve<IJobManager>().RunAll(System.Threading.CancellationToken.None);
            //var success = result.FirstOrDefault().Success ? "Success" : "Fail";
        }

        public Command ViewBrochureCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new Brochures());
                });
            }
        }

       
        public Command PromoterAttendanceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new Views.Attendance.PromoterAttendance());
                });
            }
        }

        public Command SalesTargetCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                   
                    Shell.Current.FlyoutIsPresented = false;
                    MonthlyTargetView monthlyTargetView = new MonthlyTargetView();
                 await   monthlyTargetView.GetLocationbyPersonId();
                    await Navigation.PushAsync(monthlyTargetView);
                });
            }
        }

        public Command SalesReturnCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new SalesDataReturnEntry());
                });
            }
        }

        public Command InventoryStockCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new InventoryStockDetails(1));
                });
            }
        }

        public Command OutOfStockCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new InventoryStockDetails(2));
                });
            }
        }

        public Command AttendanceTrackerCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new AttendanceSummaries());
                });
            }
        }

        public Command TargetsOverviewCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new TargetsOverview());
                });
            }
        }


        public Command ReportsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new ReportsView());
                });
            }
        }

        public Command PriceTrackerCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new PriceTrackerView());
                });
            }
        }

        public Command DisplayTrackerCommand
        {
            get
            {
                return new Command(() =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    Navigation.PushAsync(new DisplayTrackerView());
                });
            }
        }

        #region check user status
        public int CheckValidUser()
        {
            int logoutModel = 0;

            #region SQLite connection check
            var conn = DependencyService.Get<ISQLite>().GetConnection();
            if (string.IsNullOrWhiteSpace(conn.DatabasePath.ToString()))
            {
                logoutModel = (int)LogoutDataEnum.SqlLiteConnectionIssue;
            }
            #endregion

            #region concurrency check

            #endregion

            return logoutModel;
        }
        #endregion
        public Command SupervisorVisitsandTasksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Shell.Current.FlyoutIsPresented = false;
                    await Navigation.PushAsync(new VisitsAndTasksView());
                });
            }
        }
    }
}


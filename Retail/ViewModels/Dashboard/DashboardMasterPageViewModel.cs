using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Retail.Views;
using Retail.Views.Attendance;
using Retail.Views.Dashboard;
using Retail.Views.InventoryStock;
using Retail.Views.MarketIntelligence;
using Retail.Views.MyVisits;
using Retail.Views.PriceTracker;
using Retail.Views.Reports;
using Retail.Views.SalesTargetViews;
using Retail.Views.SupervisorFlow;
using Xamarin.Forms;

namespace Retail.ViewModels.Dashboard
{
    public class DashboardMasterPageViewModel : BaseViewModel
    {
        public ICommand SelectTileCommand { get; set; }
        

        public DashboardMasterPageViewModel(INavigation navigation) : base(navigation)
        {
            //IsBusy = false;

            DashboardMasterTiles();

            SelectTileCommand = new Command(SelectionTileChangedEvent);
           

            Device.BeginInvokeOnMainThread(async () => {
                CommonAttribute.UserLocation = await Extensions.GetGeolocation();
                if (CommonAttribute.UserLocation == null)
                {
                    await ErrorDisplayAlert("Your gps location is not accurate.");
                }

                await HubConnection();
            });

           
        }

        public Command UserNotificationsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new NotificationPage());
                    //Navigation.PushAsync(new PlannedVisitsView());
                });
            }
        }



        HubConnection hubConnection;
        private async Task HubConnection()
        {
            //if (!IsConnected)
            //{
                await Disconnect();
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(ServiceEndPoints.WebAppUri + "chatter").Build();
                hubConnection.On<ChatMessage>("ReceiveMessage", async (chatMessage) =>
                {
                //Console.WriteLine("Received signalR");
                if (chatMessage.UserName == CommonAttribute.CustomeProfile?.Email)
                    {
                        await LogoutUser((int)LogoutDataEnum.ConcurrencyTimeout);
                    }
                });

                await Connect();
            //}
           
            //if (hubConnection.State == HubConnectionState.Connected)
            //{
            //    hubConnection.SendAsync("MoveFromServer", e.RawX, e.RawY);
            //}
        }

        public async Task Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    await hubConnection.StartAsync();
                    //await hubConnection.InvokeAsync("SendMessage");

                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                IsConnected = false;
                //await App.Current.MainPage.DisplayAlert("Error", "Connection lost, Connect to the internet and try again", "Ok");
            }
        }

        public async Task Disconnect()
        {
            try
            {
                if (IsConnected)
                {
                    await hubConnection.StopAsync();
                    //await hubConnection.InvokeAsync("SendMessage");

                    IsConnected = false;
                }
            }
            catch (Exception e)
            {
                IsConnected = false;
                //await App.Current.MainPage.DisplayAlert("Error", "Connection lost, Connect to the internet and try again", "Ok");
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
            }
        }

        private void SelectionTileChangedEvent(object obj)
        {
            List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();
            var DashboarMasterTile1 = (DashboarMasterTile)obj;
            if (DashboarMasterTile1.Label1 == "Visits & Tasks")
            {
               
                //if (lstRoleIds.Contains((int)PersonRoleEnum.Promoter) || lstRoleIds.Contains((int)PersonRoleEnum.FieldServiceAgent))
                //{
                    Navigation.PushAsync(new PlannedVisitsView());
                //}
                //else if(lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager))
                //{
                //    Navigation.PushAsync(new VisitsAndTasksView());
                //}
            }
            if(DashboarMasterTile1.Label1 == "Supervisor Visits")
            {
                //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager))
                    Navigation.PushAsync(new VisitsAndTasksView());
            }
            if (DashboarMasterTile1.Label1 == "Sales Target")
                Navigation.PushAsync(new MonthlyTargetView());
            if (DashboarMasterTile1.Label1 == "Sales Entry")
                Navigation.PushAsync(new SalesDataEntry());
            if (DashboarMasterTile1.Label1 == "Sales Return")
                Navigation.PushAsync(new SalesDataReturnEntry());
            if (DashboarMasterTile1.Label1 == "My Attendance")
                Navigation.PushAsync(new PromoterAttendance());
            if (DashboarMasterTile1.Label1 == "Market Intel")
                Navigation.PushAsync(new MarketIntelligenceView());
            if (DashboarMasterTile1.Label1 == "Reports")
                Navigation.PushAsync(new ReportsView());
            if (DashboarMasterTile1.Label1 == "Targets Overview")
                Navigation.PushAsync(new TargetsOverview());
            if (DashboarMasterTile1.Label1 == "Attendance Tracking")
                Navigation.PushAsync(new AttendanceSummaries());
            if (DashboarMasterTile1.Label1 == "Out of Stock")
                Navigation.PushAsync(new AddInventoryStock());
            if (DashboarMasterTile1.Label1 == "Price Tracker")
                Navigation.PushAsync(new PriceTrackerView());
            if (DashboarMasterTile1.Label1 == "Display Tracker")
                Navigation.PushAsync(new DisplayTrackerView());

        }

        public void DashboardMasterTiles()
        {
            List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();
            ObservableCollection<DashboarMasterTile> dashboarMasterTiles = new ObservableCollection<DashboarMasterTile>();
            List<MobileMainMenuResponse> DBMobileAppModules = (List<MobileMainMenuResponse>)CommonAttribute.CustomeProfile.MobileMainMenus;

            if (DBMobileAppModules != null && DBMobileAppModules.Count > 0 && lstRoleIds != null && lstRoleIds.Count > 0)
            {
                var DBMobileAppModulesAsc = DBMobileAppModules.OrderBy(p => p.MenuOrder).ToList();

                foreach (var MobileAppModule in DBMobileAppModulesAsc)
                {
                    switch (MobileAppModule.Name)
                    {
                        case "Visits & Tasks":
                            //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || (lstRoleIds.Contains((int)PersonRoleEnum.Manager)) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "locationdashboard.png",
                                Label1 = "Visits & Tasks"
                            });

                            //}
                            //else if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)))
                            //{
                            //    dashboarMasterTiles.Add(new DashboarMasterTile
                            //    {
                            //        SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                            //        Image1 = "locationiconblue.png",
                            //        Label1 = "Visits and Tasks"
                            //    });
                            //}
                            break;

                        case "Supervisor Visits": //"Out of Stock"
                                                  //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                                                  //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "locationdashboard.png",
                                Label1 = "Supervisor Visits"
                            });
                            //}
                            break;

                        case "Out of Stock Entry": //"Out of Stock"
                                                   //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                                                   //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "inventorydashboard.png",
                                Label1 = "Out of Stock"
                            });
                            //}
                            break;

                        case "Sales Target":
                            //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "targetdashboard.png",
                                Label1 = "Sales Target"
                            });
                            //}
                            break;

                        case "Sales Data Entry":
                            //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "salesentrydashboard.png",
                                Label1 = "Sales Entry"
                            });
                            //}
                            break;

                        case "Sales Return":
                            //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "salesreturnsadhboard.png",
                                Label1 = "Sales Return"
                            });
                            //}
                            break;

                        case "Attendance":
                            //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "attendancedashboard.png",
                                Label1 = "My Attendance"
                            });
                            //}
                            break;

                        case "Market Insights":
                            //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            IsMarketInsights = true;
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "marketinteldashboard.png",
                                Label1 = "Market Intel"
                            });
                            //}
                            break;

                        case "Attendance Tracking":
                            //if ((lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) || (lstRoleIds.Contains((int)PersonRoleEnum.Manager)) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "attendancedashboard.png",
                                Label1 = "Attendance Tracking"
                            });
                            //}
                            break;

                        case "Reports":
                            //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "reportsdashbaord.png",
                                Label1 = "Reports"
                            });
                            //}
                            break;

                        case "Targets Overview":
                            //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "targetdashboard.png",
                                Label1 = "Targets Overview"
                            });
                            //}
                            break;

                        case "Price Tracker Entry":
                            //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "pricetracker.png",
                                Label1 = "Price Tracker"
                            });
                            //}
                            break;

                        case "Display Tracker Entry":
                            //if (lstRoleIds.Contains((int)PersonRoleEnum.Supervisor) || lstRoleIds.Contains((int)PersonRoleEnum.Manager) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                            //{
                            dashboarMasterTiles.Add(new DashboarMasterTile
                            {
                                SrNo1 = Convert.ToInt32(MobileAppModule.MenuOrder),
                                Image1 = "displaytracker.png",
                                Label1 = "Display Tracker"
                            });
                            //}
                            break;

                    }

                }

                if (dashboarMasterTiles != null && dashboarMasterTiles.Count > 0)
                {
                    var list1 = dashboarMasterTiles.OrderBy(p => p.SrNo1).ToList();
                    DashboarMasterTileList = new ObservableCollection<DashboarMasterTile>(list1);
                }
            }
        }

        public Command ScanBarcodeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new Barcode());
                });
            }
        }

        public Command ScanQRCodeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new QRCode());
                });
            }
        }
        public Command MarketInsightsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new MarketInsights());
                });
            }
        }

        public Command QuestionnaireCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.Questionnaire);
                });
            }
        }

        public Command ProductTestCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.ProductTest);
                });
            }
        }

        public Command SurveyCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.Survey);
                });
            }
        }

        private async void NavigateCommandSubmit(int MarketIntelTypeId)
        {
            await Navigation.PushAsync(new ActiveMarketIntelList(MarketIntelTypeId));
           
        }

        public ObservableCollection<DashboarMasterTile> _DashboarMasterTileList;
        public ObservableCollection<DashboarMasterTile> DashboarMasterTileList
        {
            get { return _DashboarMasterTileList; }
            set
            {
                _DashboarMasterTileList = value;
                OnPropertyChanged(nameof(DashboarMasterTileList));
            }
        }

    }
}


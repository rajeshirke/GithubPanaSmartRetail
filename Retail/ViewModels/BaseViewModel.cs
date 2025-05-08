using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Retail.Resx;
using Retail.Views.Account;
using Syncfusion.SfBusyIndicator.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand ChangeLangugeCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand LogoutWithoutConfirmCommand { get; set; }

        public BaseViewModel(INavigation navigation)
        {
            if (Application.Current.Properties.ContainsKey("userpic"))
            {
                UserPic = Application.Current.Properties["userpic"] as ImageSource;
            }
            else
                UserPic = "defultuser";

            Device.BeginInvokeOnMainThread(async () => {
                CheckLocationAvailability();
                var networkAvailable = CheckNetwrokAvailability();
                if (!networkAvailable)
                {
                    IsBusy = false;                    
                    //showToasterNoNetwork();
                }
                
            });

            if (CommonAttribute.CustomeProfile != null)
            {
                Person = CommonAttribute.CustomeProfile;
                CurrencyCode = CommonAttribute.CustomeProfile?.CurrencyCode;
                TaxRate = Convert.ToDecimal(CommonAttribute.CustomeProfile.CountryResponse?.CountryLevelSettingResponse?.TaxPercentage);
            }
            VersionNumber = VersionTracking.CurrentVersion;
            ScreenWidth = CommonAttribute.ScreenWidth;
            ScreenHeight = CommonAttribute.ScreenHeight;
            Navigation = navigation;
            if (CommonAttribute.selectedLang.LongCode == "ur")
            {
                flowDirection = FlowDirection.RightToLeft;
                BtnRotationBack = 180;
            }
            else
            {
                flowDirection = FlowDirection.LeftToRight;
                BtnRotationBack = 0;
            }

            ChangeLangugeCommand = new Command<string>((val) =>
            {
                try
                {
                    LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo(val));
                    var languages = Extensions.Getlanguages();

                    CommonAttribute.selectedLang = languages.Find(u => u.LongCode == val);
                    CultureInfo cultureInfo = new CultureInfo(val);
                    if (val == "ur")
                        flowDirection = FlowDirection.RightToLeft;
                    else
                        flowDirection = FlowDirection.LeftToRight;

                    MessagingCenter.Send<BaseViewModel>(this, "Lang");
                }
                catch (Exception ex)
                {
                    Debugger.Log(0, null, ex.ToString());
                }             
            });

            BackCommand = new Command(() =>
            {
                Navigation.PopAsync();
            });

            #region manual logout
            LogoutCommand = new Command(async () =>
            {
                LogoutDataModel.LogoutUser = (int) LogoutDataEnum.ManualLogout;
                await LogoutUser(LogoutDataModel.LogoutUser);
            });
            #endregion

            #region other than manual logout is apprear here
            LogoutWithoutConfirmCommand = new Command(async (logoutModel) => {
                await LogoutUser((int)logoutModel);
            });
            #endregion

            VersionNumber = VersionTracking.CurrentVersion;
            ScreenWidth = CommonAttribute.ScreenWidth;
            ScreenHeight = CommonAttribute.ScreenHeight;
            Navigation = navigation;
            Navigation = navigation;

        }

        HubConnection hubConnection;
        private async Task HubConnection()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://devsrv01.panasonic.ae:92/chatter").Build();
            hubConnection.On<ChatMessage>("ReceiveMessage", async (chatMessage) =>
            {
                if (chatMessage.UserName == CommonAttribute.CustomeProfile?.Email)
                {
                    await LogoutUser((int)LogoutDataEnum.ConcurrencyTimeout);
                }
            });

            await Connect();           
        }

        public async Task Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    await hubConnection.StartAsync();

                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                IsConnected = false;
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

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public bool NetworkAvailable { get { return CheckNetwrokAvailability(); } }

        public bool CheckNetwrokAvailability()
        {
            bool networkAvailable = false;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                networkAvailable = true;              
            }

            return networkAvailable;
        }

        public void showToasterNoNetwork()
        {
            var messageToast = DependencyService.Get<IMessage>();
            messageToast.DismissAlert();
            messageToast.ShortAlert("You are offline");
        }

        public void showToasterMessage(string msg)
        {
            var messageToast = DependencyService.Get<IMessage>();
            messageToast.DismissAlert();
            messageToast.ShortAlert(msg);
        }

        public async void CheckLocationAvailability()
        {
            try
            {
                //var location = await Geolocation.GetLastKnownLocationAsync();
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                else
                {
                    await DisplayAlert("", "Location not enabled");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                ShowLocationTrackingIssue();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                ShowLocationTrackingIssue();
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                ShowLocationTrackingIssue();
            }
            catch (Exception ex)
            {
                // Unable to get location
                ShowLocationTrackingIssue();
            }
        }

        private async void ShowLocationTrackingIssue()
        {
            //await DisplayAlert("", "Location tracking issue");
        }

        public async Task LogoutUser(int logoutModel)
        {
            var res = false;
            if (logoutModel == (int)LogoutDataEnum.ManualLogout)
            {
                res = await YesorNoDisplayAlert("Alert!", "Are you sure you want to logout ?");
            }
            else if (logoutModel == (int)LogoutDataEnum.SessionTimeout)
            {
                await ErrorDisplayAlert("Session timeout");
                res = true;
            }
            else if (logoutModel == (int)LogoutDataEnum.ConcurrencyTimeout)
            {
                await ErrorDisplayAlert("Forcefully Logged out.");
                res = true;
            }
            else if (logoutModel == (int)LogoutDataEnum.SqlLiteConnectionIssue)
            {
                await ErrorDisplayAlert("SQLite connection issue");
                res = true;
            }
            else
            {
                await ErrorDisplayAlert("Something went wrong");
                res = true;
            }

            if (res)
            {
                //Application.Current.Properties.Remove("email");
                //Application.Current.Properties.Remove("password");
                //Application.Current.Properties.Clear();

                //clearing commonattribute
                CommonAttribute.CustomeProfile = null;

                //clearing properties
                Application.Current.Properties.Remove("SyncDateTime");
                Application.Current.Properties.Remove("CheckinToday");
                Application.Current.Properties.Remove("SupervisorflowSubmitToday");
                Application.Current.Properties.Remove("userpic");
                Application.Current.Properties.Remove("Photoaction");
                Application.Current.Properties.Remove("AppOpened");
                Application.Current.Properties.Remove("SupWorkflowOffline");

                //clearing local db
                Connection c = new Connection();
                //c.clear();
                c.NoClearSupData();

                //clearing garbage space
                GC.Collect();

                await Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        public async Task ErrorDisplayAlert(string msg)
        {
            await Application.Current.MainPage.DisplayAlert("", msg, AppResources.lblOk);
        }
        //
        public async Task DisplayAlert(string title, string msg)
        {
            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }
        public async Task<bool> YesorNoDisplayAlert(string title, string msg)
        {
            return await Application.Current.MainPage.DisplayAlert(title, msg, AppResources.lblOk, AppResources.lblCancel);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        SfBusyIndicator busyIndicator = new SfBusyIndicator()
        {
            AnimationType = AnimationTypes.DoubleCircle,
            ViewBoxHeight = 150,
            ViewBoxWidth = 150,
            TextColor = Color.Blue,
            BackgroundColor=Color.White
            
        };

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public int BtnRotationBack { get; set; }       
        private string _CurrencyCode;
        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                _CurrencyCode = value;
                OnPropertyChanged("CurrencyCode");
            }
        }
        private decimal _TaxRate;
        public decimal TaxRate
        {
            get { return _TaxRate; }
            set
            {
                _TaxRate = value;
                OnPropertyChanged("TaxRate");
            }
        }

        private PersonLoginResponse _Person;
        public PersonLoginResponse Person
        {
            get { return _Person; }
            set
            {

                _Person = value;
                OnPropertyChanged("Person");
            }
        }

        public string _VersionNumber;
        public string VersionNumber
        {
            get { return _VersionNumber; }
            set
            {

                _VersionNumber = value;
                OnPropertyChanged("VersionNumber");


            }
        }

        public Rectangle _HomeCircle;
        public Rectangle HomeCircle
        {
            get {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    _HomeCircle = new Rectangle(0f, 0f, 500, 400);
                }
                else if (Device.Idiom == TargetIdiom.Tablet)
                {
                    _HomeCircle = new Rectangle(0f, 0f, 1200, 400);
                }
                else
                {
                    _HomeCircle = new Rectangle(0f, 0f, 500, AbsoluteLayout.AutoSize);
                }
                return _HomeCircle;
            }
            set
            {
                _HomeCircle = value;
                OnPropertyChanged("HomeCircle");
            }
        }

        private ImageSource _UserPic;
        public ImageSource UserPic
        {
            get { return _UserPic; }
            set
            {
                _UserPic = value;
                OnPropertyChanged("UserPic");
            }
        }

        public bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                var hud = DependencyService.Get<IEWProgress>();
                if (value)
                {
                    hud.Show();
                }
                else
                {
                    hud.Dismiss();
                }
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public TextAlignment _textAlignment;
        public TextAlignment textAlignment
        {
            get { return _textAlignment; }
            set
            {

                _textAlignment = value;
                OnPropertyChanged("textAlignment");


            }
        }

        public FlowDirection _flowDirection;
        public FlowDirection flowDirection
        {
            get { return _flowDirection; }
            set
            {
                CommonAttribute.flowDirection = value;
                if (value == FlowDirection.LeftToRight)
                    textAlignment = TextAlignment.Start;
                else
                    textAlignment = TextAlignment.Start;

                _flowDirection = value;
                OnPropertyChanged("flowDirection");
            }
        }

        private int _ListHeight;
        public int ListHeight
        {
            get { return _ListHeight; }
            set
            {
                _ListHeight = value;
                OnPropertyChanged(nameof(ListHeight));
            }
        }

        private int _ScreenWidth;
        public int ScreenWidth
        {
            get { return _ScreenWidth; }
            set
            {
                _ScreenWidth = value;
                OnPropertyChanged("ScreenWidth");
            }
        }

        private int _ScreenHeight;
        public int ScreenHeight
        {
            get { return _ScreenHeight; }
            set
            {
                _ScreenHeight = value;
                OnPropertyChanged("ScreenHeight");
            }
        }


        private int _ScreenHeightSize;
        public int ScreenHeightSize
        {
            get {
                DisplayOrientation orientation = DeviceDisplay.MainDisplayInfo.Orientation;
                if (orientation == DisplayOrientation.Portrait)
                {
                    _ScreenHeightSize = ScreenHeight / 2;
                } else
                {
                    _ScreenHeightSize = ScreenWidth / 2;
                }
                return _ScreenHeightSize;
            }
        }

        #region dashboard left side menues

        private bool _IsSalesTarget = false;
        public bool IsSalesTarget
        {
            get { return _IsSalesTarget; }
            set
            {

                _IsSalesTarget = value;
                OnPropertyChanged(nameof(IsSalesTarget));
            }
        }

        private bool _IsSalesEntry = false;
        public bool IsSalesEntry
        {
            get { return _IsSalesEntry; }
            set
            {

                _IsSalesEntry = value;
                OnPropertyChanged(nameof(IsSalesEntry));
            }
        }

        private bool _IsSalesReturn = false;
        public bool IsSalesReturn
        {
            get { return _IsSalesReturn; }
            set
            {

                _IsSalesReturn = value;
                OnPropertyChanged(nameof(IsSalesReturn));
            }
        }

        private bool _IsAttendance = false;
        public bool IsAttendance
        {
            get { return _IsAttendance; }
            set
            {

                _IsAttendance = value;
                OnPropertyChanged(nameof(IsAttendance));
            }
        }

        private bool _IsInventory = false;
        public bool IsInventory
        {
            get { return _IsInventory; }
            set
            {

                _IsInventory = value;
                OnPropertyChanged(nameof(IsInventory));
            }
        }

        private bool _IsAttendanceTracker = false;
        public bool IsAttendanceTracker
        {
            get { return _IsAttendanceTracker; }
            set
            {

                _IsAttendanceTracker = value;
                OnPropertyChanged(nameof(IsAttendanceTracker));
            }
        }

        private bool _IsTargetsOverview = false;
        public bool IsTargetsOverview
        {
            get { return _IsTargetsOverview; }
            set
            {

                _IsTargetsOverview = value;
                OnPropertyChanged(nameof(IsTargetsOverview));
            }
        }

        private bool _IsVisitsandTasks = false;
        public bool IsVisitsandTasks
        {
            get { return _IsVisitsandTasks; }
            set
            {

                _IsVisitsandTasks = value;
                OnPropertyChanged(nameof(IsVisitsandTasks));
            }
        }

        private bool _IsProductCatalogue = false;
        public bool IsProductCatalogue
        {
            get { return _IsProductCatalogue; }
            set
            {

                _IsProductCatalogue = value;
                OnPropertyChanged(nameof(IsProductCatalogue));
            }
        }

        private bool _IsReports = false;
        public bool IsReports
        {
            get { return _IsReports; }
            set
            {

                _IsReports = value;
                OnPropertyChanged(nameof(IsReports));
            }
        }

        private bool _IsMarketInsights = false;
        public bool IsMarketInsights
        {
            get { return _IsMarketInsights; }
            set
            {

                _IsMarketInsights = value;
                OnPropertyChanged(nameof(IsMarketInsights));
            }
        }

        private bool _IsDashboard = false;
        public bool IsDashboard
        {
            get { return _IsDashboard; }
            set
            {

                _IsDashboard = value;
                OnPropertyChanged(nameof(IsDashboard));
            }
        }

        private bool _IsSupervisorVisits = false;
        public bool IsSupervisorVisits
        {
            get { return _IsSupervisorVisits; }
            set
            {

                _IsSupervisorVisits = value;
                OnPropertyChanged(nameof(IsSupervisorVisits));
            }
        }

        private bool _IsPriceTracker = false;
        public bool IsPriceTracker
        {
            get { return _IsPriceTracker; }
            set
            {

                _IsPriceTracker = value;
                OnPropertyChanged(nameof(IsPriceTracker));
            }
        }

        private bool _IsDisplayTracker = false;
        public bool IsDisplayTracker
        {
            get { return _IsDisplayTracker; }
            set
            {

                _IsDisplayTracker = value;
                OnPropertyChanged(nameof(IsDisplayTracker));
            }
        }

        private bool _IsOutOfStock = false;
        public bool IsOutOfStock
        {
            get { return _IsOutOfStock; }
            set
            {
                _IsOutOfStock = value;
                OnPropertyChanged(nameof(IsOutOfStock));
            }
        }

        #region Reports Based on User
        private bool _IsPromoterAttendanceReportVisible = false;
        public bool IsPromoterAttendanceReportVisible
        {
            get { return _IsPromoterAttendanceReportVisible; }
            set
            {

                _IsPromoterAttendanceReportVisible = value;
                OnPropertyChanged(nameof(IsPromoterAttendanceReportVisible));
            }
        }

        private bool _IsDailySalesByCategoryReportVisible = false;
        public bool IsDailySalesByCategoryReportVisible
        {
            get { return _IsDailySalesByCategoryReportVisible; }
            set
            {

                _IsDailySalesByCategoryReportVisible = value;
                OnPropertyChanged(nameof(IsDailySalesByCategoryReportVisible));
            }
        }

        private bool _IsPromoterwiseAchievementReportVisible = false;
        public bool IsPromoterwiseAchievementReportVisible
        {
            get { return _IsPromoterwiseAchievementReportVisible; }
            set
            {

                _IsPromoterwiseAchievementReportVisible = value;
                OnPropertyChanged(nameof(IsPromoterwiseAchievementReportVisible));
            }
        }

        private bool _IsSalesReportVisible = false;
        public bool IsSalesReportVisible
        {
            get { return _IsSalesReportVisible; }
            set
            {

                _IsSalesReportVisible = value;
                OnPropertyChanged(nameof(IsSalesReportVisible));
            }
        }

        private bool _IsVisitTargetsReportVisible = false;
        public bool IsVisitTargetsReportVisible
        {
            get { return _IsVisitTargetsReportVisible; }
            set
            {

                _IsVisitTargetsReportVisible = value;
                OnPropertyChanged(nameof(IsVisitTargetsReportVisible));
            }
        }

        private bool _IsCategorywiseContributionReportVisible = false;
        public bool IsCategorywiseContributionReportVisible
        {
            get { return _IsCategorywiseContributionReportVisible; }
            set
            {

                _IsCategorywiseContributionReportVisible = value;
                OnPropertyChanged(nameof(IsCategorywiseContributionReportVisible));
            }
        }

        #endregion
        //public NetworkAccess NetworkState { get; set; }

        #region Bottom tabs
        private bool _IsVisitsandTasksBottomTab = false;
        public bool IsVisitsandTasksBottomTab
        {
            get { return _IsVisitsandTasksBottomTab; }
            set
            {
                _IsVisitsandTasksBottomTab = value;
                OnPropertyChanged(nameof(IsVisitsandTasksBottomTab));
            }
        }

        private bool _IsSupervisorVisitsBottomTab = false;
        public bool IsSupervisorVisitsBottomTab
        {
            get { return _IsSupervisorVisitsBottomTab; }
            set
            {
                _IsSupervisorVisitsBottomTab = value;
                OnPropertyChanged(nameof(IsSupervisorVisitsBottomTab));
            }
        }

        private bool _IsSalesEntryBottomTab = false;
        public bool IsSalesEntryBottomTab
        {
            get { return _IsSalesEntryBottomTab; }
            set
            {
                _IsSalesEntryBottomTab = value;
                OnPropertyChanged(nameof(IsSalesEntryBottomTab));
            }
        }

        private bool _IsTargetsOverviewBottomTab = false;
        public bool IsTargetsOverviewBottomTab
        {
            get { return _IsTargetsOverviewBottomTab; }
            set
            {
                _IsTargetsOverviewBottomTab = value;
                OnPropertyChanged(nameof(IsTargetsOverviewBottomTab));
            }
        }

        private bool _IsReportsBottomTab = false;
        public bool IsReportsBottomTab
        {
            get { return _IsReportsBottomTab; }
            set
            {
                _IsReportsBottomTab = value;
                OnPropertyChanged(nameof(IsReportsBottomTab));
            }
        }

        #endregion

        #endregion
    }
}


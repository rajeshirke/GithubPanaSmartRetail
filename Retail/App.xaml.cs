using System;
using System.Globalization;
using Retail.Views;
using Retail.Views.Dashboard;
using Retail.Views.SalesTargetViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Retail.Hepler;
using System.Threading.Tasks;
using Extensions = Retail.Hepler.Extensions;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.Enums;
using Retail.Views.Account;
using Retail.Database;
using Xamarin.Essentials;
using Retail.Resx;
using Retail.ViewModels.Dashboard;
using Retail.ViewModels.Attendance;
using System.Diagnostics;
using Retail.DependencyServices;
using System.Linq;
using Shiny;
using System.Threading;
using Plugin.DeviceInfo;

[assembly: ExportFont("US101.ttf")]
[assembly: ExportFont("timesnewroman.ttf")]
[assembly: ExportFont("Zachery.ttf")]
namespace Retail
{
    public partial class App : Application
    {
        const string AppOpened = "AppOpened";
        Connection c = new Connection();

        public App()
        {
            LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("en-US"));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg3MjY2QDMxMzkyZTM0MmUzMFFTNnZlWEF4Q0xDK01sUzRZVHB0dkNaVVFIUkhqTkttYm9KaklMTWV2YWs9");
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            c.con();
        }

        public async Task<bool> LoginPage()
        {
            bool results = false;

            if (NetworkAvailable)
            {
                var languages = Extensions.Getlanguages();
                CommonAttribute.selectedLang = languages[0];
                LoginRequest loginRequest = new LoginRequest();
#if DEBUG
                {
                    loginRequest.email = "Navod@mastcgroup.com";// Current.Properties["email"].ToString();
                loginRequest.username = "Navod@mastcgroup.com";// Current.Properties["email"].ToString();
                    loginRequest.password = "Password.01";// Current.Properties["password"].ToString();
                }

#else
             loginRequest.email = Current.Properties["email"].ToString();
                loginRequest.username = Current.Properties["email"].ToString();
                loginRequest.password = Current.Properties["password"].ToString();
#endif


                string DeviceID = CrossDeviceInfo.Current.Id.ToString();
                string DeviceModel = CrossDeviceInfo.Current.Model.ToString();
                string Platform = CrossDeviceInfo.Current.Platform.ToString();
                string Version = CrossDeviceInfo.Current.Version.ToString();
                var AppVersion = CrossDeviceInfo.Current.AppVersion.ToString().Replace(".", "");


                LoginInputModel loginInputModel = new LoginInputModel();
                loginInputModel.Email = Current.Properties["email"].ToString(); ;
                loginInputModel.Username = Current.Properties["email"].ToString(); ;
                loginInputModel.Password = Current.Properties["password"].ToString(); ;
                loginInputModel.DeviceID = DeviceID;
                loginInputModel.DeviceModel = DeviceModel;
                loginInputModel.DevicePlatform = Platform;
                loginInputModel.OSVersion = Version;
                loginInputModel.AppVersion = Convert.ToInt32(AppVersion);

                UserManagementSL userManagementSL = new UserManagementSL();

                ////old API
                //ReceiveContext<PersonLoginResponse> receiveContext = await userManagementSL.ValidateUser(loginRequest);

                ////Concurrent login API
                ReceiveContext<PersonLoginResponse> receiveContext = await userManagementSL.ConcurrentLogin(loginInputModel);
                if (receiveContext?.status == Convert.ToInt16(APIResponseEnum.Success))
                {
                    //if (receiveContext.data.PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
                    //{
                        CommonAttribute.CustomeProfile = receiveContext.data;
                        results = true;
                    //Application.Current.MainPage = new DashBoadMasterPage();
                    //}
                }
            }
            else
            {
                var messageToast = DependencyService.Get<IMessage>();
                messageToast.DismissAlert();
                messageToast.ShortAlert("You are offline");
            }
            return results;
        }

        protected async override void OnStart()
        {
            Console.WriteLine("RIAA_Mob App 199 Onstrat :" + DateTime.Now);
            await AuthenticateUser();
            Console.WriteLine("RIAA_Mob after AuthenticateUser :" + DateTime.Now);
            AppOpenedDateTime("OnStart");

            // iOS App Privacy
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var appTrackingTransparencyPermission = DependencyService.Get<IAppTrackingTransparencyPermission>();
                var status = await appTrackingTransparencyPermission.CheckStatusAsync();

                if (status != PermissionStatus.Granted)
                    appTrackingTransparencyPermission.RequestAsync(s => MyImplementation(s));
                else
                    MyImplementation(status);
            }
            else
            {
                // app center or other implementations
                #region AppCenter
                AppCenter.Start("ios=92c2447f-fe02-47ee-9a1c-af188bd585c5;" +
                         "uwp=e68ca6c9-1b4f-4364-a486-e673758cc7dc;" +
                         "android=47338f1d-a480-4ce3-92e7-8b7cea925e73",
                         typeof(Analytics), typeof(Crashes));
                #endregion
            }

            await GetCurrentLocation();
        }

        private void MyImplementation(PermissionStatus status)
        {
            if (status == PermissionStatus.Granted)
            {
                // app center or other implementations
                #region AppCenter
                AppCenter.Start("ios=92c2447f-fe02-47ee-9a1c-af188bd585c5;" +
                         "uwp=e68ca6c9-1b4f-4364-a486-e673758cc7dc;" +
                         "android=47338f1d-a480-4ce3-92e7-8b7cea925e73",
                         typeof(Analytics), typeof(Crashes));
                #endregion
            }
        }

        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }

        protected override void OnSleep()
        {
            ClearBackgroundApps();
        }

        protected override void OnResume()
        {
            AppOpenedDateTime("OnResume");
        }

        private void AppOpenedDateTime(string param)
        {
            try
            {
                //BackgroundTasks();
                RunShinyJobsAsync();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private async Task RunShinyJobsAsync()
        {
            var accessStatus = await ShinyHost.Resolve<Shiny.Notifications.INotificationManager>().RequestAccess();
            var result = await ShinyHost.Resolve<Shiny.Jobs.IJobManager>().RunAll(System.Threading.CancellationToken.None);
            var success = result.FirstOrDefault().Success ? "Success" : "Fail";
        }

        private void ClearBackgroundApps()
        {
            try
            {
                //GC.Collect();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private async Task AuthenticateUser()
        {
            if (Current.Properties.ContainsKey("email") && Current.Properties.ContainsKey("password"))
            {
                try
                {
                    if (NetworkAvailable)
                    {
                        bool res = await LoginPage();
                        if (res && CommonAttribute.CustomeProfile?.PersonId > 0)
                        {
                            NavigateToDashboard();
                        }
                        else
                        {
                            NavigateToLoginPage();
                        }
                    }
                    else if (CommonAttribute.CustomeProfile?.PersonId > 0)
                    {
                        NavigateToDashboard();
                    }
                    else
                    {
                        NavigateToLoginPage();
                    }
                }
                catch (Exception ex)
                {
                    NavigateToLoginPage();
                }
            }
            else
            {
                NavigateToLoginPage();
            }
        }

        private void NavigateToDashboard()
        {
            Application.Current.Properties[AppOpened] = DateTime.Today;
            MainPage = new DashboardMenuPage();
        }

        private void NavigateToLoginPage()
        {
            var navigation = new NavigationPage(new LoginPage());

            navigation.BarBackgroundColor = Color.FromHex("#2D3EE1");
            MainPage = navigation;// new TechMasterPage(); //navigation;
        }


        //Location tracking
        CancellationTokenSource cts;
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (Xamarin.Essentials.PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        //protected override void OnDisappearing()
        //{
        //    if (cts != null && !cts.IsCancellationRequested)
        //        cts.Cancel();
        //    base.OnDisappearing();
        //}
    }
}

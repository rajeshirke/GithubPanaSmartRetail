using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Resx;
using Retail.Views.Account;
using Retail.Views.Dashboard;
using Shiny.Jobs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.DeviceInfo;


namespace Retail.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        const string AppOpened = "AppOpened";
        Connection c = new Connection();
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            try
            {
                IsPassword = true;
                IspsswordSave = true;
                languages = Extensions.Getlanguages();

                

                Device.BeginInvokeOnMainThread(async () => {
                    await BindingData();
                    IsBusy = false;
                });
                IsBusy = false;
            }
            catch (Exception ex)
            {

            }
        }
        public async Task BindingData()
        {
            try
            {
                CommonAttribute.selectedLang = languages[0];
                //if (Application.Current.Properties.ContainsKey("email") && Application.Current.Properties.ContainsKey("password"))
                //{
                //    if (loginRequest == null)
                //        loginRequest = new LoginRequest();

                //    loginRequest.email = Application.Current.Properties["email"].ToString();
                //    loginRequest.username = Application.Current.Properties["email"].ToString();
                //    loginRequest.password = Application.Current.Properties["password"].ToString();
                //  //  await SiginSubmit();
                //}
            }
            catch (Exception ex)
            {

            }           
        }
        public LoginRequest loginRequest { get; set; }
        public LoginInputModel loginInputModel { get; set; }
        public async Task SiginSubmit()
        {
            try
            {
                IsBusy = true;
                //Kumar Adabala edit
               // await Task.Delay(1000);
                Analytics.TrackEvent("SiginSubmit Event");

                if (NetworkAvailable)
                {
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                      //  await Task.Delay(20);
                        UserManagementSL userManagementSL = new UserManagementSL();
                        if (loginRequest == null)
                            loginRequest = new LoginRequest();

                        if (loginInputModel == null)
                        {
                            loginInputModel = new LoginInputModel();
                        }

#if (DEBUG)
                        //if (string.IsNullOrEmpty(Email))
                        //{
                        //    Email = "adabalakumar9@gmail.com";
                        //    Password = "kumar1";
                        //}
                        //else
                        //{
                        //    Email = "akash.tech2@gmail.com";
                        //    Password = "Password.01";
                        //}
#else
                             loginRequest.email = Email;
                             loginRequest.username = Email;
                             loginRequest.password = Password;
#endif

                        if (!string.IsNullOrEmpty(Email))
                        {
                            Email = Email.Trim();
                        }

                        loginRequest.email = Email;
                        loginRequest.username = Email;
                        loginRequest.password = Password;

                        string DeviceID = CrossDeviceInfo.Current.Id.ToString();
                        string DeviceModel = CrossDeviceInfo.Current.Model.ToString();
                        string Platform = CrossDeviceInfo.Current.Platform.ToString();
                        string Version = CrossDeviceInfo.Current.Version.ToString();
                        var AppVersion = CrossDeviceInfo.Current.AppVersion.ToString().Replace(".", "");

                        loginInputModel.Email = Email;
                        loginInputModel.Username = Email;
                        loginInputModel.Password = Password;
                        loginInputModel.DeviceID = DeviceID;
                        loginInputModel.DeviceModel = DeviceModel;
                        loginInputModel.DevicePlatform = Platform;
                        loginInputModel.OSVersion = Version;
                        loginInputModel.AppVersion = Convert.ToInt32(AppVersion);

                        if (!await validation())
                        {
                            IsBusy = false;
                            return;
                        }

                        ////For concurrent login
                        ReceiveContext<PersonLoginResponse> receiveContext = await userManagementSL.ConcurrentLogin(loginInputModel);


                        //ReceiveContext<PersonLoginResponse> receiveContext = await userManagementSL.ValidateUser(loginRequest);
                        ////role tech

                        if (receiveContext?.status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            //clearing
                            Application.Current.Properties.Clear();
                            Application.Current.Properties.Remove("email");
                            Application.Current.Properties.Remove("password");

                            CommonAttribute.Custpwd = loginRequest.password;
                            Application.Current.Properties["currentpassword"] = loginRequest.password;
                            //  CommonAttribute.Custpwd = Password;

                            #region DB data
                                //*******for clearing db data of the user if logged in by different user on same device 
                                var ExistUser = Preferences.Get("PersonId", (long)0);
                                if (Preferences.ContainsKey("PersonId"))
                                {
                                    if (receiveContext.data.PersonId == ExistUser)
                                    {

                                    }
                                    else
                                    {
                                        Connection c = new Connection();
                                        c.clear();
                                    }
                                    Preferences.Set("PersonId", receiveContext.data.PersonId);
                                }
                                else
                                {
                                    Preferences.Set("PersonId", receiveContext.data.PersonId);
                                }
                            #endregion

                            #region remember password
                            if (IspsswordSave)
                            {
                                //adding
                                Application.Current.Properties["email"] = loginRequest.email;
                                Application.Current.Properties["password"] = loginRequest.password;
                            } else
                            {
                                //Application.Current.Properties["email"] = "";
                                //Application.Current.Properties["password"] = "";
                                Application.Current.Properties.Remove("password");
                            }
                            #endregion

                            if (receiveContext.data.PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
                            {
                                CommonAttribute.CustomeProfile = receiveContext.data;

                                Application.Current.Properties[AppOpened] = DateTime.Today;
                                Application.Current.MainPage = new DashboardMenuPage();
                            }
                            else if (receiveContext.data.PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
                            {
                                CommonAttribute.CustomeProfile = receiveContext.data;

                                //Application.Current.MainPage = new TechMasterPage();
                            }

                            CommonAttribute.CustomeProfile = receiveContext.data;

                            
                            Application.Current.Properties[AppOpened] = DateTime.Today;
                            Application.Current.MainPage = new DashboardMenuPage();

                            // for syncing
                          //  BackgroundTask backgroungTask = new BackgroundTask();
                          //  backgroungTask.BackgroundClearings();
                          //  backgroungTask.BackgroundTasks();


                           // IsBusy = false;
                        }
                        else if (receiveContext != null)
                        {
                            IsBusy = false;
                            if (!receiveContext.errorMessage.ToLower().Contains("create password"))
                                await Application.Current.MainPage.DisplayAlert("", receiveContext.errorMessage, "Cancel");
                            else
                                await Application.Current.MainPage.DisplayAlert("", receiveContext.errorMessage, "Cancel");
                        }
                        else
                        {
                            await ErrorDisplayAlert(AppResources.servererror);
                            IsBusy = false;
                        }

                  //  });
                }
                else
                {
                    showToasterNoNetwork();
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
        public async Task<bool> validation()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(Resx.AppResources.lblErrorTitle, "Please enter email or mobile number.", Resx.AppResources.lblCancel);
                return false;
            }
           
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(Resx.AppResources.lblErrorTitle, "Please check entered email/mobile and password.", Resx.AppResources.lblCancel);
                return false;
            }
            return true;
        }

       
        //
        #region Events
        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new SignUpPage());
                });
            }
        }
        public Command ForgotPwdCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new ForgotPasswordPage(Email));
                });
            }
        }
        public Command SelectedSavePasswordCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IspsswordSave)
                        IspsswordSave = false;
                    else
                        IspsswordSave = true;
                });
            }
        }
        public Command SignInCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //if (await validation())
                    //{
                    await SiginSubmit();
                    // }

                });
            }
        }
        public Command IsPasswordCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsPassword = !IsPassword;
                });
            }
        }
        #endregion

        #region Properties

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        private bool _IspsswordSave;
        public bool IspsswordSave
        {
            get { return _IspsswordSave; }
            set
            {
                _IspsswordSave = value;
                OnPropertyChanged("IspsswordSave");
            }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }
        private bool _IsPassword;
        public bool IsPassword
        {
            get { return _IsPassword; }
            set
            {
                _IsPassword = value;
                OnPropertyChanged(nameof(IsPassword));
                OnPropertyChanged(nameof(IsPasswordIcon));
            }
        }
        private string _IsPasswordIcon;
        public string IsPasswordIcon
        {
            get {
                if (IsPassword)
                {
                    _IsPasswordIcon = "eyeshow";
                } else
                {
                    _IsPasswordIcon = "eyehide";
                }
                return _IsPasswordIcon;
            }
        }
        public List<LanguageModel> languages { get; set; }
        public LanguageModel SelectedLanguage { get; set; }
        #endregion
    }
}


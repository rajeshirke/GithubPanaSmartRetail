using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
using Xamarin.Forms;

namespace Retail.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            try
            {
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
        public async Task SiginSubmit()
        {
              //Application.Current.MainPage = new DashboardMenuPage();

            try
            {
                IsBusy = true;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(20);
                    UserManagementSL userManagementSL = new UserManagementSL();
                    if (loginRequest == null)
                        loginRequest = new LoginRequest();
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

                    loginRequest.email = Email;
                    loginRequest.username = Email;
                    loginRequest.password = Password;

                    if (!await validation())
                    {
                        IsBusy = false;
                        return;
                    }
                    ReceiveContext<PersonLoginResponse> receiveContext = await userManagementSL.ValidateUser(loginRequest);
                    //role tech 
                    if (receiveContext?.status == Convert.ToInt16(APIResponseEnum.Success))
                    {
                        CommonAttribute.Custpwd = loginRequest.password;
                        Application.Current.Properties["currentpassword"] = loginRequest.password;
                        //  CommonAttribute.Custpwd = Password;
                        if (IspsswordSave)
                        {
                            Application.Current.Properties["email"] = loginRequest.email;
                            Application.Current.Properties["password"] = loginRequest.password;
                        }
                        //if (receiveContext.data.PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
                        //{
                        //    CommonAttribute.CustomeProfile = receiveContext.data;

                        //    Application.Current.MainPage = new DashboardMenuPage();
                        //}
                        //else if (receiveContext.data.PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
                        //{
                        //    CommonAttribute.CustomeProfile = receiveContext.data;

                        //    //Application.Current.MainPage = new TechMasterPage();
                        //}

                        CommonAttribute.CustomeProfile = receiveContext.data;
                        Application.Current.MainPage = new DashboardMenuPage();
                        IsBusy = false;
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

                });

            }
            catch (Exception ex)
            {

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
        public List<LanguageModel> languages { get; set; }
        public LanguageModel SelectedLanguage { get; set; }
        #endregion
    }
}


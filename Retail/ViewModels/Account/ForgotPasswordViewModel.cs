using System;
using System.Windows.Input;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.Account;
using Retail.Views.CommonPages;
using Xamarin.Forms;

namespace Retail.ViewModels.Account
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        public ForgotPasswordViewModel(INavigation navigation) : base(navigation)
        {
            BindingData();
        }
        public void BindingData()
        {
            SuccessView = false;
            FirstView = true;
            SendRequestCommand = new Command(async () =>
            {
                if (validation())
                {
                    IsBusy = true;
                    UserManagementSL userManagement = new UserManagementSL();
                    ForgotPasswordRequest forgotPassword = new ForgotPasswordRequest();
                    forgotPassword.username = Email;
                    var receiveContext = await userManagement.ForgotPasswordEmail(forgotPassword);
                    if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                    {
                        IsBusy = false;
                        SuccessView = true;
                        FirstView = false;                        
                        await Navigation.PushAsync(new FeedBackSuccessPage("ForgetPassword"));
                        //await Navigation.PushAsync(new VerifyPhonePage("forgot"));
                    }
                    else if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Failure))
                    {
                        IsBusy = false;

                        await Application.Current.MainPage.DisplayAlert("Error!", receiveContext.ErrorMessage, "Ok");
                    }
                    else if (receiveContext != null)
                    {
                        IsBusy = false;

                        await Application.Current.MainPage.DisplayAlert("Error!", receiveContext.ErrorMessage, "Ok");
                    }
                    else
                    {
                        await ErrorDisplayAlert("API Error");
                        IsBusy = false;
                    }

                }
                // Navigation.PushAsync(new ForgotPasswordPage());
            });
        }
        public ICommand SendRequestCommand { get; set; }
        public bool validation()
        {
            if (string.IsNullOrEmpty(Email))
            {
                // await Application.Current.MainPage.DisplayAlert(Resx.AppResources.lblErrorTitle, , Resx.AppResources.lblCancel);
                Application.Current.MainPage.DisplayAlert(Resx.AppResources.lblErrorTitle, "Please enter email or mobile number.", Resx.AppResources.lblCancel);
                return false;
            } else
            {
                Email = Email.Trim();
            }

            return true;
        }
        public Command SignInCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();

                });
            }
        }
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
        private bool _SuccessView;
        public bool SuccessView
        {
            get { return _SuccessView; }
            set
            {
                _SuccessView = value;
                OnPropertyChanged("SuccessView");
            }
        }
        private bool _FirstView;
        public bool FirstView
        {
            get { return _FirstView; }
            set
            {
                _FirstView = value;
                OnPropertyChanged("FirstView");
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

    }
}


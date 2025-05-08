using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.Dashboard;
using Xamarin.Forms;

namespace Retail.ViewModels.Account
{
    public class ForgotPagePasswordViewModel : BaseViewModel
    {
        public ForgotPagePasswordViewModel(INavigation navigation) : base(navigation)
        {
            Name = CommonAttribute.CustomeProfile.FirstName + " " + CommonAttribute.CustomeProfile.LastName;
        }
        public Command SubmitCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        await ErrorDisplayAlert("Please enter password.");
                        return;
                    }
                    var hasMiniMaxChars = new Regex(@".{4,15}");
                    if (!hasMiniMaxChars.IsMatch(NewPassword))
                    {
                        await ErrorDisplayAlert("New Password should not be less than 8 or greater than 12 characters");
                        return;
                    }
                    if (string.IsNullOrEmpty(ConfirmPassword))
                    {
                        await ErrorDisplayAlert("Please enter confirm password.");
                        return;
                    }
                    if (NewPassword != ConfirmPassword)
                    {
                        await ErrorDisplayAlert("The password and confirmation password do not match.");
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        if (NetworkAvailable)
                        {
                            UserManagementSL userManagementSL = new UserManagementSL();
                            PasswordRequestModel passwordRequestModel = new PasswordRequestModel();
                            passwordRequestModel.Email = CommonAttribute.CustomeProfile.Email;
                            passwordRequestModel.CurrentPassword = "";
                            passwordRequestModel.ConfirmPassword = ConfirmPassword;
                            passwordRequestModel.NewPassword = NewPassword;
                            passwordRequestModel.Language = CommonAttribute.selectedLang.LongName;
                            passwordRequestModel.UserID = CommonAttribute.CustomeProfile.UserId;
                            var receiveContext = await userManagementSL.ChangePassword(passwordRequestModel);
                            if (receiveContext?.status == Convert.ToInt16(APIResponseEnum.Success))
                            {
                                Application.Current.MainPage = new DashboardMasterPage();
                            }
                            else if (receiveContext != null)
                            {
                                await Application.Current.MainPage.DisplayAlert("", receiveContext.errorMessage, "Cancel");
                            }
                            else
                            {
                                await ErrorDisplayAlert("API Error");
                            }
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

                });
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _NewPassword;
        public string NewPassword
        {
            get { return _NewPassword; }
            set
            {
                _NewPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set
            {
                _ConfirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
    }
}


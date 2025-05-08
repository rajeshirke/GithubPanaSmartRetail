using System;
using System.Text.RegularExpressions;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Hepler;
using Retail.ViewModels;
using Xamarin.Forms;
using System.Diagnostics;
using Retail.DependencyServices;

namespace Retail.ViewModels.Account
{
    public class ChnagePasswordViewModel : BaseViewModel
    {
        public ChnagePasswordViewModel(INavigation navigation) : base(navigation)
        {
            IsPassword = true;
            Email = CommonAttribute.CustomeProfile.Email;
            Name = CommonAttribute.CustomeProfile.FirstName + "" + CommonAttribute.CustomeProfile.LastName;
            //CommonAttribute.Custpwd

        }
        public Command UpdateCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (string.IsNullOrEmpty(Password))
                    {
                        await ErrorDisplayAlert("Please enter current password.");
                        return ;
                    }
                    string cPassword  = Application.Current.Properties["currentpassword"].ToString();
                    if (Password != cPassword)
                    {
                        await ErrorDisplayAlert("Current password is incorrect.");
                        return;
                    }
                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        await ErrorDisplayAlert("Please enter new password.");
                        return;
                    }
                    if (Password == NewPassword)
                    {
                        await ErrorDisplayAlert("The new password should not be same as old password.");
                        return;
                    }
                   
                    if (NewPassword != null)
                    {
                        var hasMiniMaxChars = new Regex(@".{4,15}");
                        if (!hasMiniMaxChars.IsMatch(NewPassword))
                        {
                            await ErrorDisplayAlert("New new password should not be less than 8 or greater than 12 characters");
                            return ;
                        }
                    }
                    if (string.IsNullOrEmpty(ConfirmPassword))
                    {
                        await ErrorDisplayAlert("Please enter confirm password.");
                        return ;
                    }
                    if (NewPassword != ConfirmPassword)
                    {
                        await ErrorDisplayAlert("The new password and confirmation password do not match.");
                        return ;
                    }

                    try
                    {
                        IsBusy = true;

                        if (NetworkAvailable)
                        {
                            UserManagementSL userManagementSL = new UserManagementSL();
                            PasswordRequestModel passwordRequestModel = new PasswordRequestModel();
                            passwordRequestModel.Email = CommonAttribute.CustomeProfile.Email;
                            passwordRequestModel.CurrentPassword = Password;
                            passwordRequestModel.ConfirmPassword = ConfirmPassword;
                            passwordRequestModel.NewPassword = NewPassword;
                            passwordRequestModel.Language = CommonAttribute.selectedLang.LongName;
                            passwordRequestModel.UserID = CommonAttribute.CustomeProfile.UserId;
                            var receiveContext = await userManagementSL.ChangePassword(passwordRequestModel);
                            if (receiveContext?.status == Convert.ToInt16(APIResponseEnum.Success))
                            {
                                await DisplayAlert("Success", receiveContext.message);
                                await Navigation.PopAsync();
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
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than 8 or greater than 12 characters";
                return false;
            }
            else if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
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
            get
            {
                if (IsPassword)
                {
                    _IsPasswordIcon = "eyeshow";
                }
                else
                {
                    _IsPasswordIcon = "eyehide";
                }
                return _IsPasswordIcon;
            }
        }
    }
}

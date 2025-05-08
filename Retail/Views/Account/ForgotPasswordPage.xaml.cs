using System;
using System.Collections.Generic;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordViewModel viewModel { get; set; }
        public ForgotPasswordPage(string email)
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1E55A5");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            
            BindingContext = viewModel = new ForgotPasswordViewModel(Navigation);
            viewModel.Email = email;

        }
    }
}

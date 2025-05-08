using System;
using System.Collections.Generic;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpViewModel viewModel { get; set; }
        public SignUpPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1E55A5");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            BindingContext = viewModel = new SignUpViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();           
        }
    }
}

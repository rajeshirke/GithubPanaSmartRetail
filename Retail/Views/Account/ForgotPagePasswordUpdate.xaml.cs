using System;
using System.Collections.Generic;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class ForgotPagePasswordUpdate : ContentPage
    {
        public ForgotPagePasswordUpdate()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);// = "False"
            BindingContext = new ForgotPagePasswordViewModel(Navigation);
        }
    }
}

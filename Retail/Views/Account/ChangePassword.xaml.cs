using System;
using System.Collections.Generic;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
            BindingContext = new ChnagePasswordViewModel(Navigation);
        }
    }
}

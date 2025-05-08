using System;
using System.Collections.Generic;
using Retail.ViewModels.ProfileView;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class SingUpProfileUpdatePage : ContentPage
    {
        public SingUpProfileUpdatePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);// = "False"
            BindingContext = new MyProfileViewModel(Navigation);
        }
    }
}

using System;
using System.Collections.Generic;
using Retail.ViewModels.ProfileView;
using Xamarin.Forms;

namespace eWarranty.Views.Customer.UserProfile
{
    public partial class ProfileUpdatePage : ContentPage
    {
        public ProfileUpdatePage()
        {
            InitializeComponent();
            BindingContext = new MyProfileViewModel(Navigation);
        }
    }
}

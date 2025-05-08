using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Retail.Hepler;
using Retail.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Retail.ViewModels.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Retail.Views.Account;
using eWarranty.Views.Customer.UserProfile;
using System.Diagnostics;
using Retail.Views.Dashboard;

namespace Retail.Views.Account
{
    public partial class MyProfileNew : ContentPage
    {
        MyProfilePageViewModel viewModel;        

        public MyProfileNew()
        {
            InitializeComponent();
            BindingContext = viewModel = new MyProfilePageViewModel(Navigation);

            if (CommonAttribute.CustomeProfile.ProfilePictureFileInfo != null)
            {
                if (Application.Current.Properties.ContainsKey("Photoaction") && Application.Current.Properties["Photoaction"] != null)
                {
                    if (Application.Current.Properties["Photoaction"].ToString() == "Take Photo")
                        PhotoImage.Rotation = 90;
                }

                PhotoImage.Source = CommonAttribute.ImageBaseUrl + CommonAttribute.CustomeProfile.ProfilePictureFileInfo.Path;
            }
            else
                PhotoImage.Source = "userdashbaord";
        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        void ChnageTapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {

                FileData fileData = await viewModel.TakePhotoAsync();
                if (!string.IsNullOrEmpty(fileData?.string64baseData))
                {

                    viewModel.UserPic = ImageSource.FromStream(
               () => new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                    PhotoImage.Source = viewModel.UserPic;

                    MessagingCenter.Send<MyProfileNew, ImageSource>(this, "UserPhoto", viewModel.UserPic);
                    if (viewModel.Photoaction != "Gallery")
                        PhotoImage.Rotation = 90;
                    else
                        PhotoImage.Rotation = 0;
                    await viewModel.SaveUserPic(fileData);
                }
                viewModel.IsBusy = false;
            });
        }
    }
}

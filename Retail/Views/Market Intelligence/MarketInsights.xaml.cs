using System;
using System.Collections.Generic;
using System.IO;
using Retail.Models;
using Retail.ViewModels.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence
{
    public partial class MarketInsights : ContentPage
    {
        MarketInsightsViewModel viewModel { get; set; }
        public MarketInsights()
        {
            InitializeComponent();
            BindingContext = viewModel= new MarketInsightsViewModel(Navigation);
            
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            //viewModel.IsBusy = true;
            //Device.BeginInvokeOnMainThread(async () =>
            //{

            //    FileData fileData = await viewModel.TakePhotoAsync();
            //    if (!string.IsNullOrEmpty(fileData?.string64baseData))
            //    {

            //        viewModel.UserPic = ImageSource.FromStream(
            //   () => new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
            //        CapturedImage.Source = viewModel.UserPic;

            //        MessagingCenter.Send<MarketInsights, ImageSource>(this, "UserPhoto", viewModel.UserPic);
            //        if (viewModel.Photoaction != "Gallery")
            //            CapturedImage.Rotation = 90;
            //        else
            //            CapturedImage.Rotation = 0;
            //        await viewModel.SaveUserPic(fileData);
            //    }
            //    viewModel.IsBusy = false;


            //});

        }
    }
}

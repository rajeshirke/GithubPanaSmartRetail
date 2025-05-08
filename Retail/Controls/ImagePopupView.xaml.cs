using System;
using System.Collections.Generic;
using Retail.DependencyServices;
using Extensions = Retail.Hepler;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Threading.Tasks;
using Application = Xamarin.Forms.Application;
using Retail.ViewModels.ImagePopUp;

namespace Retail.Controls
{
    public partial class ImagePopupView : ContentPage
    {
        string _imageURL = "";
        public ImagePopupView(string imageUrl)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            
            TapGestureRecognizer tapEvent = new TapGestureRecognizer();
            tapEvent.Tapped += (sender, e) => {
                Navigation.PopModalAsync(true);
            };

            CancelImg.GestureRecognizers.Add(tapEvent);

            ZoomImage.Source = imageUrl.Replace("Uri: ", "");
            //sZoomImage.Source = imageUrl.Replace("File: ", "");
            _imageURL = imageUrl.Replace("Uri: ", "");
            BindingContext = new ImagePopupViewModel(Navigation, _imageURL);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Retail.Controls;
using Retail.ViewModels.ProductCatalogue;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.Views.ProductCatalogue
{
    public partial class Brochures : ContentPage
    {
       
        public Brochures()
        {
            InitializeComponent();
            BindingContext = new BrochureViewModel(Navigation);
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var popupImage = (Image)sender;            
            
             string currentUrl = popupImage.Source.ToString();
             await Navigation.PushModalAsync(new ImagePopupView(currentUrl), true);

        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            var PDF = (Label)sender;
            string fileUrl = PDF.Text;

            try
            {
                Uri uri = new Uri(fileUrl);

                await Browser.OpenAsync(uri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.FromHex("#1E55A5"),
                    PreferredControlColor = Color.DeepPink,
                });

                //await Navigation.PushAsync(new PDFViewerPage(fileUrl, "PDF Viewer", "pdf"));
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }
    }
}

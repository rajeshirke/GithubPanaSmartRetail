using System;
using System.Collections.Generic;
using Retail.DependencyServices;
using Xamarin.Forms;

namespace Retail.Views.ProductCatalogue
{
    public partial class PDFViewerPage : ContentPage
    {
        public string PageUrl { get; set; }
        public string FileType { get; set; }

        public PDFViewerPage(string Url)
        {
            InitializeComponent();
            DependencyService.Get<IClearCookies>().Clear();
            if (Device.RuntimePlatform == Device.Android)
            {
                wbPDF.Source = Url;
               
            }
            else
                wbPDF.Source = "https://drive.google.com/viewerng/viewer?embedded=true&url=" + Url;

            PageUrl = Url;
        }

        public PDFViewerPage(string url, string title, string filrtype = "pdf")
        {
            
            InitializeComponent();
            DependencyService.Get<IClearCookies>().Clear();           
            Title = title;
            FileType = filrtype;
            if (FileType.Contains("image"))
            {
                wbPDF.Source = url;
            }
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    wbPDF.Source = url;               
                }
                else
                    wbPDF.Source = "https://drive.google.com/viewerng/viewer?embedded=true&url=" + url;
            }
            PageUrl = url;
        }

        void wbPDF_PropertyChanging(System.Object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            IsBusy = false;
            lblstatus.IsVisible = false;
            DependencyService.Get<IClearCookies>().Clear();
        }

        void wbPDF_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
        }
    }
}

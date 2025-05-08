using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class TermsofServicePage : ContentPage
    {
        public TermsofServicePage(string pageUrl)
        {
            InitializeComponent();
            wvData.Source = pageUrl;
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    //HttpClient hpClient = new HttpClient();
            //    //HttpResponseMessage requestMessage = await hpClient.GetAsync(pageUrl);
            //    //string HTMLContent = await requestMessage.Content.ReadAsStringAsync();

            //    //lblName.Text = HTMLContent;
            //});

        }
        //  string textData = "We are pleased that you have elected to visit this Website (referred to herein as the “Website” or the “Site”), operated by, or on behalf of Panasonic Corporation of North America.Please read these Terms and Conditions of Use (“Terms and Conditions”) carefully and ensure you understand them. These Terms and Conditions, including all documents referenced herein, represent the entire understanding and agreement between Panasonic and you regarding your use of this Website and supersede any prior statements or representations. This Website’s Privacy Policy is incorporated into these Terms and Conditions by reference and is made a part hereof. https://shop.panasonic.com/pna-privacy-policies.html. You are not authorized to use the Website if you do not agree to all or any of these Terms and Conditions. Access to, distribution and/or use of this Website is subject to all applicable laws and regulations.To the extent that access to, distribution and/or use of this Website would be deemed illegal by applicable law, such access, distribution and/or use is prohibited.Each time you visit any area on the Website and/or register for any interactive functionality of the Website, you are deemed to have confirmed your acceptance to these Terms and Conditions and the Website’s Privacy Policy https://shop.panasonic.com/pna-privacy-policies.html. If you do not agree to abide by these Terms and Conditions and the Privacy Policy, please do not use this Website.";
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void WebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }
    }
}

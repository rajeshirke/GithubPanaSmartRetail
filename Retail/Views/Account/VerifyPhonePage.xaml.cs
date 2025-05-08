using System;
using System.Collections.Generic;
using System.Linq;
using Retail.Hepler;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class VerifyPhonePage : ContentPage
    {
        public VerifyPhoneViewModel viewModel { get; set; }

        public VerifyPhonePage(string pageName)
        {

            InitializeComponent();
            btnVerfy.IsEnabled = false;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1E55A5");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            //  float maxtime = 2.50f;
            lbltime.BindingContext = this;
            BindingContext = viewModel = new VerifyPhoneViewModel(Navigation, pageName);
            //userToken.email = CommonAttribute.CustomeProfile.Email;
            //userToken.phoneNumber = CommonAttribute.CustomeProfile.MobileNumber;
            string emailmsg = "OTP sent to ";
            if (!string.IsNullOrEmpty(CommonAttribute.CustomeProfile.Email) && !string.IsNullOrEmpty(CommonAttribute.CustomeProfile.MobileNumber))
            {
                emailmsg = emailmsg + CommonAttribute.CustomeProfile.Email + " & " + CommonAttribute.CustomeProfile.MobileNumber;
            }
            else if (!string.IsNullOrEmpty(CommonAttribute.CustomeProfile.Email) && string.IsNullOrEmpty(CommonAttribute.CustomeProfile.MobileNumber))
            {
                emailmsg = emailmsg + CommonAttribute.CustomeProfile.Email;
            }
            else if (string.IsNullOrEmpty(CommonAttribute.CustomeProfile.Email) && !string.IsNullOrEmpty(CommonAttribute.CustomeProfile.MobileNumber))
            {
                emailmsg = emailmsg + CommonAttribute.CustomeProfile.MobileNumber;
            }
            lblemailmsg.Text = emailmsg;
            NewOtpCode();
            btnnewCode.IsEnabled = false;
            btnVerfy.IsEnabled = false;

        }
        protected override bool OnBackButtonPressed() => false;

        public void NewOtpCode()
        {
            btnVerfy.IsEnabled = false;
            SecondsElapsed = 0;
            int MaxSeconds = 180;
            TimeSpan timeSpan;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                if (SecondsElapsed < MaxSeconds)
                {
                    SecondsElapsed++;
                    timeSpan = new TimeSpan(0, 0, ((MaxSeconds - SecondsElapsed)));
                    lbltime.Text = timeSpan.Minutes.ToString() + ":" + timeSpan.Seconds.ToString();
                    GC.SuppressFinalize(this);
                    //update the count down timer with 1 second here 
                    return true;
                }
                btnVerfy.IsEnabled = false;
                btnnewCode.IsEnabled = true;
                btnnewCode.BackgroundColor = Color.Silver;
                return false;

            });
        }
        public int SecondsElapsed { get; set; }

        void btnnewCode_Clicked(System.Object sender, System.EventArgs e)
        {
            btnnewCode.IsEnabled = false;
            btnnewCode.BackgroundColor = Color.White;
            viewModel.IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                await viewModel.ResendOtp();
                IsBusy = false;
            });

            NewOtpCode();
        }
        private void Peso_Completed(object sender, EventArgs e)
        {
            var entry = sender as Entry; // .. and check for null
            var list = (entry.Parent as Grid).Children; //assumes a StackLayout
            var index = list.IndexOf(entry); // what if IndexOf returns -1?
            var nextIndex = (index + 1) >= list.Count ? 0 : index + 1; //first or next element?
            var next = list.ElementAt(nextIndex);
            next?.Focus();
        }

        void Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                var entry = sender as Entry;
                var list = (entry.Parent as Grid).Children; //assumes a StackLayout
                var index = list.IndexOf(entry); // what if IndexOf returns -1?
                var nextIndex = (index + 1) >= list.Count ? 0 : index + 1; //first or next element?
                var next = list.ElementAt(nextIndex);
                if (index != 3)
                    next?.Focus();
                else
                    btnVerfy.IsEnabled = true;
            }
            checkOtpnumber();
        }
        public void checkOtpnumber()
        {
            if (string.IsNullOrEmpty(txt1.Text) || string.IsNullOrEmpty(txt2.Text) || string.IsNullOrEmpty(txt3.Text) || string.IsNullOrEmpty(txt4.Text))
            {
                btnVerfy.IsEnabled = false;
            }
            else
                btnVerfy.IsEnabled = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {

        }
    }
}

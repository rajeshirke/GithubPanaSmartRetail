using System;
using System.Collections.Generic;
using System.Linq;
using Retail.Views.Account;
using Retail.Views.Dashboard;
using Retail.Views.PriceTracker;
using Retail.Views.SalesTargetViews;
using Xamarin.Forms;

namespace Retail.Views.CommonPages
{
    public partial class FeedBackSuccessPage : ContentPage
    {
        const string AppOpened = "AppOpened";

        string Page = "";
        string SuccessSalesEntry = "Sales data saved successfully.";
        string SuccessInventory = "Inventory Stock information Saved successfully.";
        string SuccessOutofStock = "Out of Stock information Saved successfully.";
        string SuccessSalesReturn = "Sales return data saved successfully.";
        string SuccessMarketInsights = "Details are saved successfully.";
        string SuccessForgetPassword = "Password sent successfully.";
        string SuccessPriceTracker = "Price Tracker Entries Saved Successfully.";
        string SuccessDisplayTracker = "Display Tracker Entries Saved Successfully.";

        public string Desc { get; set; }

        public FeedBackSuccessPage(string pageName)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetBackButtonTitle(this, "");
            

            Page = pageName;
            if (pageName == "SalesDataEntry")
            {
                lblMsg.Text = SuccessSalesEntry;
                Button1.Text = "Go To Sales Entry"; //"Go To List";
            }
            else if (pageName == "SalesDataReturnEntry")
            {
                lblMsg.Text = SuccessSalesReturn;
                Button1.Text = "Go To Home"; //"Go To List";
            }
            else if (pageName == "Inventory")
            {
                lblMsg.Text = SuccessInventory;
            }
            else if (pageName == "MarketInsights")
            {
                lblMsg.Text = SuccessMarketInsights;
            }
            else if (pageName == "ForgetPassword")
            {
                lblMsg.Text = SuccessForgetPassword;
                Button1.Text = "Go to login";
            }
            else if (pageName == "OutOfStock")
            {
                lblMsg.Text = SuccessOutofStock;
            }
            else if (pageName == "Price Tracker")
            {
                lblMsg.Text = SuccessPriceTracker;
                Button1.Text = "Go To Price Tracker";
            }
            else if (pageName == "Display Tracker")
            {
                lblMsg.Text = SuccessDisplayTracker;
                Button1.Text = "Go To Display Tracker";
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Button1.Text == "Go To Sales Entry")
            {

                await Navigation.PushAsync(new SalesDataEntry());

                //Navigation.InsertPageBefore(new MonthlyTargetView(),
                //    Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                //await Navigation.PopAsync();

            }
            else if (Button1.Text == "Go to login")
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else if (Button1.Text == "Go To Price Tracker")
            {
                await Navigation.PushAsync(new PriceTrackerView());
            }
            else if (Button1.Text == "Go To Display Tracker")
            {
                await Navigation.PushAsync(new DisplayTrackerView());
            }
            else
            {
                Application.Current.Properties[AppOpened] = DateTime.Today;
                Application.Current.MainPage = new DashboardMenuPage();
            }
                    
        }
    }
}

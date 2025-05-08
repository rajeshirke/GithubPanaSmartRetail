using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Retail.Infrastructure.RequestModels;
using Retail.ViewModels.Reports;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.Reports
{
    public partial class DailySalesTrendBySubcategory : ContentPage
    {
        public DailySalesTrendBySubcategoryViewModel viewModel { get; set; }
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }
        public ObservableCollection<SelectedSubcategories> SelectedSubcategories { get; set; }
        public ObservableCollection<AccountListBasedonSelectedCity> SelectedAccounts { get; set; }
        public string SelectedMonthDate { get; set; }
        public ToolbarItem toolbarItem { get; set; }

        public DailySalesTrendBySubcategory()
        {
            InitializeComponent();
            BindingContext = viewModel = new DailySalesTrendBySubcategoryViewModel(Navigation);

            toolbarItem = new ToolbarItem
            {
                Text = "dashbord",
                IconImageSource = ImageSource.FromFile("chart.png"),
                Order = ToolbarItemOrder.Primary,
                Command = viewModel.ChartClick,
                Priority = 0,
            };
            this.ToolbarItems.Add(toolbarItem);

            var firstDayOfMonth = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1);
            SelectedMonthDate = firstDayOfMonth.ToString();

            var mindate = new DateTime(2000, 01, 01);
            var maxdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);

            monthyearpicker.MinDate = mindate;
            monthyearpicker.MaxDate = maxdate;
        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            Expander expander = label.Parent.Parent.Parent as Expander;

            if (label.FontSize == Device.GetNamedSize(NamedSize.Default, label))
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
            }
            else
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Default, label);
            }
            expander.ForceUpdateSize();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string>(this, "OnlyTwoMonthYearChangeDate", async (selectedItems) =>
            {   
                string selecteddate = Convert.ToDateTime(selectedItems).ToShortDateString();                
                viewModel.SelectedDate = String.Format("{0:yyyy-MM-dd}", selecteddate);
                SelectedMonthDate= selecteddate.ToString();
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedCountryLists", async (selectedItems) =>
            {
                string CountryNames = "";
                SelectedCountries = new ObservableCollection<SelectedCountries>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        SelectedCountries.Add(new SelectedCountries { ID = item.ID, Name = item.Name });
                        CountryNames = CountryNames + item.Name + ",";
                    }
                       
                }
                txtCountries.Text = CountryNames.TrimEnd(',');

                txtAccounts.Text = "";
                SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedAccountLists", async (selectedItems) =>
            {
                string AccountNames = "";
                SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        SelectedAccounts.Add(new AccountListBasedonSelectedCity { ID = item.ID, Name = item.Name });
                        AccountNames = AccountNames + item.Name + ",";
                    }                        
                }
                txtAccounts.Text = AccountNames.TrimEnd(',');
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedSubcategoryLists", async (selectedItems) =>
            {
                string Category = "";
                SelectedSubcategories = new ObservableCollection<SelectedSubcategories>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        SelectedSubcategories.Add(new SelectedSubcategories { ID = item.ID, Name = item.Name });
                        Category = Category + item.Name + ",";
                    }

                }
                txtSubcategories.Text = Category.TrimEnd(',');
            });
        }

        void monthyearpicker_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DateTime MonthYear = monthyearpicker.Date;
            //viewModel.SelectedDate = MonthYear.ToString("yyyy-MM-dd");
        }

        async void txtCountries_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void txtAccounts_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            List<int> SelectedCountryList = new List<int>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryList.Add(item.ID);
                }
            }
            if (SelectedCountryList != null && SelectedCountryList.Count > 0)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(7, SelectedCountryList));
            }
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            List<int> SelectedCountryList = new List<int>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryList.Add(item.ID);
                }
            }
            if (SelectedCountryList != null && SelectedCountryList.Count > 0)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(7, SelectedCountryList));
            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();

            List<int?> SelectedCountryList = new List<int?>();            
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryList.Add(item.ID);
                }
            }

            List<int?> SelectedAccountList = new List<int?>();
            if (SelectedAccounts != null && SelectedAccounts.Count > 0)
            {
                foreach (var item in SelectedAccounts)
                {
                    SelectedAccountList.Add(item.ID);
                }
            }

            List<int?> SelectedSubcategoriesList = new List<int?>();
            if (SelectedSubcategories != null && SelectedSubcategories.Count > 0)
            {
                foreach (var item in SelectedSubcategories)
                {
                    SelectedSubcategoriesList.Add(item.ID);
                }
            }

            List<DateTime?> FromDates = new List<DateTime?>();
            FromDates.Add(Convert.ToDateTime(SelectedMonthDate));

            reportCreateUpdateRequest.CountryIds = SelectedCountryList;
            reportCreateUpdateRequest.AccountIds = SelectedAccountList;
            reportCreateUpdateRequest.ProductSubCategoryID = SelectedSubcategoriesList;
            reportCreateUpdateRequest.Dates = Convert.ToDateTime(SelectedMonthDate.ToString());
            reportCreateUpdateRequest.FromDates = FromDates;

            if (reportCreateUpdateRequest != null)
            {
                await viewModel.GetDailySalesTrendBySubcategoryReports(reportCreateUpdateRequest);
            }
        }

        async void txtSubcategories_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(12));
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(12)); 
        }
    }
}

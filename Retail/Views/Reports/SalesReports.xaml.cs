using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.Reports;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.Reports
{
    public partial class SalesReports : ContentPage
    {
        public SalesTargetReportsViewModel viewModel { get; set; }
        public ToolbarItem toolbarItem { get; set; }
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }
        public ObservableCollection<Cities> SelectedCities { get; set; }
        public ObservableCollection<AccountListBasedonSelectedCity> SelectedAccounts { get; set; }
        public string SelectedMonthDate { get; set; }

        public SalesReports()
        {
            InitializeComponent();
            BindingContext = viewModel = new SalesTargetReportsViewModel(Navigation);
            rbSalesvsCountryReport.IsChecked = true;

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

            monthyearpicker1.MinDate = mindate;
            monthyearpicker1.MaxDate = maxdate;
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

        void TitledDateTimePicker_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.CurrentDate = dp.Date.ToString("yyyy-MM-dd");
        }

        void rbSalesvsCountryReport_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if(rbSalesvsCCategoryReport.IsChecked)
            {
                grSalesVrCategoryReport.IsVisible = true;
                grSalesVSCountryReport.IsVisible = false;
                SelectedCountries = new ObservableCollection<SelectedCountries>();
                SelectedCities = new ObservableCollection<Cities>();
                SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                txtCountry.Text = txtCity.Text = txtAccount.Text = "";
                viewModel.obSalesTargetCountryCityAccountReportView = new ObservableCollection<SalesTargetCountryCityAccountReportView>();

            }
            else
            {
                grSalesVSCountryReport.IsVisible = true;
                grSalesVrCategoryReport.IsVisible = false;
                SelectedCountries = new ObservableCollection<SelectedCountries>();
                SelectedCities = new ObservableCollection<Cities>();
                SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                txtCountryIds.Text = txtCityIds.Text = txtAccountIds.Text = "";
                viewModel.obSalesTargetProductCategoryReportView = new ObservableCollection<SalesTargetProductCategoryReportView>();

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string>(this, "OnlyTwoMonthYearChangeDate", async (selectedItems) =>
            {
                string selecteddate = Convert.ToDateTime(selectedItems).ToString("yyyy-MM-dd");
                var SelectedDate = String.Format("{0:yyyy-MM-dd}", selecteddate);
                SelectedMonthDate = selecteddate.ToString();
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
                if(rbSalesvsCountryReport.IsChecked)
                {
                    txtCountry.Text = CountryNames.TrimEnd(',');
                    txtCity.Text = ""; SelectedCities = new ObservableCollection<Cities>();
                    txtAccount.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                }
                else if(rbSalesvsCCategoryReport.IsChecked)
                {
                    txtCountryIds.Text=CountryNames.TrimEnd(',');
                    txtCityIds.Text = ""; SelectedCities = new ObservableCollection<Cities>();
                    txtAccountIds.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                }
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedCityLists", async (selectedItems) =>
            {
                string CityNames = "";
                SelectedCities = new ObservableCollection<Cities>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        SelectedCities.Add(new Cities { ID = item.ID, Name = item.Name });
                        CityNames = CityNames + item.Name + ",";
                    }
                    
                }
                if (rbSalesvsCountryReport.IsChecked)
                {
                    txtCity.Text = CityNames.TrimEnd(',');
                    txtAccount.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                }
                else if(rbSalesvsCCategoryReport.IsChecked)
                {
                    txtCityIds.Text = CityNames.TrimEnd(',');
                    txtAccountIds.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                }
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
                if (rbSalesvsCountryReport.IsChecked)
                {
                    txtAccount.Text = AccountNames.TrimEnd(',');
                }
                else if(rbSalesvsCCategoryReport.IsChecked)
                {
                    txtAccountIds.Text = AccountNames.TrimEnd(',');
                }
            });


        }

        async void txtCity_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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
                await Navigation.PushPopupAsync(new MultiselectPopupView(6, SelectedCountryList));
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
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
                await Navigation.PushPopupAsync(new MultiselectPopupView(6, SelectedCountryList));
            }
        }

        async void txtCountry_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void txtAccount_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
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

        async void txtCountries_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void txtCities_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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
                await Navigation.PushPopupAsync(new MultiselectPopupView(6, SelectedCountryList));
            }
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

        async void ShowSalesCategoryReports_Clicked(System.Object sender, System.EventArgs e)
        {
            List<int?> SelectedAccountIds = new List<int?>();
            if (SelectedAccounts != null && SelectedAccounts.Count > 0)
            {
                foreach(var item in SelectedAccounts)
                {
                    SelectedAccountIds.Add(item.ID);
                }
            }
            List<int?> SelectedCountryIds = new List<int?>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryIds.Add(item.ID);
                }
            }

            List<int?> SelectedCityIds = new List<int?>();
            if (SelectedCities != null && SelectedCities.Count > 0)
            {
                foreach (var item in SelectedCities)
                {
                    SelectedCityIds.Add(item.ID);
                }
            }

            List<DateTime?> FromDates = new List<DateTime?>();
            FromDates.Add(Convert.ToDateTime(SelectedMonthDate));

            List<long?> PersonIds = new List<long?>();
            PersonIds.Add(CommonAttribute.CustomeProfile?.PersonId);

            ReportCreateUpdateRequest reportCreateUpdate = new ReportCreateUpdateRequest();
            reportCreateUpdate.AccountIds = SelectedAccountIds;
            reportCreateUpdate.CountryIds = SelectedCountryIds;
            reportCreateUpdate.CityIds = SelectedCityIds;
            reportCreateUpdate.FromDates = FromDates;
            reportCreateUpdate.PersonId = PersonIds;

            if (reportCreateUpdate != null)
            {
                await viewModel.GetSalesAchievedvsCategoryReports(reportCreateUpdate);
            }

        }

        async void ShowSalesvsCountryReport_Clicked(System.Object sender, System.EventArgs e)
        {
            List<int?> SelectedAccountIds = new List<int?>();
            if (SelectedAccounts != null && SelectedAccounts.Count > 0)
            {
                foreach (var item in SelectedAccounts)
                {
                    SelectedAccountIds.Add(item.ID);
                }
            }
            List<int?> SelectedCountryIds = new List<int?>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryIds.Add(item.ID);
                }
            }

            List<int?> SelectedCityIds = new List<int?>();
            if (SelectedCities != null && SelectedCities.Count > 0)
            {
                foreach (var item in SelectedCities)
                {
                    SelectedCityIds.Add(item.ID);
                }
            }

            List<DateTime?> FromDates = new List<DateTime?>();
            FromDates.Add(Convert.ToDateTime(SelectedMonthDate));

            List<long?> PersonIds = new List<long?>();
            PersonIds.Add(CommonAttribute.CustomeProfile?.PersonId);

            ReportCreateUpdateRequest reportCreateUpdate = new ReportCreateUpdateRequest();
            reportCreateUpdate.AccountIds = SelectedAccountIds;
            reportCreateUpdate.CountryIds = SelectedCountryIds;
            reportCreateUpdate.CityIds = SelectedCityIds;
            reportCreateUpdate.FromDates = FromDates;
            reportCreateUpdate.PersonId = PersonIds;

            if (reportCreateUpdate != null)
            {   
                await viewModel.GetSalesAchievedvsCountryReports(reportCreateUpdate);
            }
        }

        async void txtCountryIds_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void txtCityIds_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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
                await Navigation.PushPopupAsync(new MultiselectPopupView(6, SelectedCountryList));
            }
        }

        async void TapGestureRecognizer_Tapped_4(System.Object sender, System.EventArgs e)
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
                await Navigation.PushPopupAsync(new MultiselectPopupView(6, SelectedCountryList));
            }
        }

        async void txtAccountIds_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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

        async void TapGestureRecognizer_Tapped_5(System.Object sender, System.EventArgs e)
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
        
    }
}



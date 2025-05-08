using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retail.Infrastructure.RequestModels;
using Retail.ViewModels.Reports;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.Reports
{
    public partial class VisitTargetsReport : ContentPage
    {
        public VisitTargetsReportViewModel viewModel { get; set; }
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }       
        public ObservableCollection<AccountListBasedonSelectedCity> SelectedAccounts { get; set; }
        public ObservableCollection<SelectedSupervisors> lstSelectedSupervisor { get; set; }
        public string SelectedMonthDate { get; set; }

        public VisitTargetsReport()
        {
            InitializeComponent();
            BindingContext = viewModel = new VisitTargetsReportViewModel(Navigation);

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

            MessagingCenter.Subscribe<string>(this, "MonthYearChangeDate", async (selectedItems) =>
            {
                string selecteddate = Convert.ToDateTime(selectedItems).ToShortDateString();
                viewModel.SelectedDate = String.Format("{0:yyyy-MM-dd}", selecteddate);
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
                txtCountry.Text = CountryNames.TrimEnd(',');
                txtAccount.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                txtSupervisor.Text = ""; lstSelectedSupervisor = new ObservableCollection<SelectedSupervisors>();
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
                txtAccount.Text = AccountNames.TrimEnd(',');
                txtSupervisor.Text = ""; lstSelectedSupervisor = new ObservableCollection<SelectedSupervisors>();
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedSupervisorList", async (selectedItems) =>
            {
                string SupNames = "";
                lstSelectedSupervisor = new ObservableCollection<SelectedSupervisors>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        lstSelectedSupervisor.Add(new SelectedSupervisors { ID = item.ID, Name = item.Name });
                        SupNames = SupNames + item.Name + ",";
                    }
                }
                txtSupervisor.Text = SupNames.TrimEnd(',');
            });

        }

        async void txtCountry_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
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

        async void txtSupervisor_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
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
            
            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();
            reportCreateUpdateRequest.CountryIds = SelectedCountryList;
            reportCreateUpdateRequest.AccountIds = SelectedAccountList;

            if (reportCreateUpdateRequest != null)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(9, reportCreateUpdateRequest,2));
            }
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
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

            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();
            reportCreateUpdateRequest.CountryIds = SelectedCountryList;
            reportCreateUpdateRequest.AccountIds = SelectedAccountList;

            if (reportCreateUpdateRequest != null)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(9, reportCreateUpdateRequest, 2));
            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
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

            List<long?> SelectedSupList = new List<long?>();
            if (lstSelectedSupervisor != null && lstSelectedSupervisor.Count > 0)
            {
                foreach (var item in lstSelectedSupervisor)
                {
                    SelectedSupList.Add(item.ID);
                }
            }

            List<DateTime?> FromDates = new List<DateTime?>();
            FromDates.Add(Convert.ToDateTime(SelectedMonthDate));

            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();
            reportCreateUpdateRequest.CountryIds = SelectedCountryList;
            reportCreateUpdateRequest.AccountIds = SelectedAccountList;
            reportCreateUpdateRequest.PersonId = SelectedSupList;
            reportCreateUpdateRequest.FromDates = FromDates;
            reportCreateUpdateRequest.Dates = Convert.ToDateTime(SelectedMonthDate.ToString());
            reportCreateUpdateRequest.SinglePersonId = null;

            if (reportCreateUpdateRequest != null)
            {
                await viewModel.GetDailyVisitTargetTasksReports(reportCreateUpdateRequest);
            }
        }

    }
}





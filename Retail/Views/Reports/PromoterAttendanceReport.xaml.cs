using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PromoterAttendanceReport : ContentPage
    {
        public PromoterAttendanceReportsViewModel viewModel { get; set; }
        public ToolbarItem toolbarItem { get; set; }
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }
        public string SelectedDate { get; set; }        
        public ObservableCollection<AccountListBasedonSelectedCity> SelectedAccounts { get; set; }
        public ObservableCollection<EntityKeyValueResponse> SelectedLocationList { get; set; }

        public PromoterAttendanceReport()
        {
            InitializeComponent();
            BindingContext = viewModel = new PromoterAttendanceReportsViewModel(Navigation);

            toolbarItem = new ToolbarItem
            {
                Text = "dashbord",
                IconImageSource = ImageSource.FromFile("chart.png"),
                Order = ToolbarItemOrder.Primary,
                //Command = viewModel.ChartClick,
                Priority = 0,
            };
            SelectedDate = DateTime.Now.ToShortDateString();
            //this.ToolbarItems.Add(toolbarItem);
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
                txtAccounts.Text = ""; SelectedAccounts = new ObservableCollection<AccountListBasedonSelectedCity>();
                txtStores.Text = ""; SelectedLocationList = new ObservableCollection<EntityKeyValueResponse>();
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
                txtStores.Text = ""; SelectedLocationList = new ObservableCollection<EntityKeyValueResponse>();
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedLocationList", async (selectedItems) =>
            {
                string LocationNames = "";
                SelectedLocationList = new ObservableCollection<EntityKeyValueResponse>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        SelectedLocationList.Add(new EntityKeyValueResponse { Id = item.ID, Name = item.Name });
                        LocationNames = LocationNames + item.Name + ",";
                    }

                }
                txtStores.Text = LocationNames.TrimEnd(',');
            });

        }

        void TitledDateTimePicker_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            SelectedDate = dp.Date.ToString("yyyy-MM-dd");
            viewModel.SelectedDate = dp.Date.ToString("yyyy-MM-dd");
        }

        async void txtCountry_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            List<int?> SelectedCountryList = new List<int?>();

            ReportCreateUpdateRequest reportCreateUpdate = new ReportCreateUpdateRequest();
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

            List<int?> SelectedStores = new List<int?>();
            if (SelectedLocationList != null && SelectedLocationList.Count > 0)
            {
                foreach (var item in SelectedLocationList)
                {
                    SelectedStores.Add(item.Id);
                }
            }
            reportCreateUpdate.CountryIds = SelectedCountryList;
            reportCreateUpdate.AccountIds = SelectedAccountList;
            reportCreateUpdate.LocationIds = SelectedStores;
            reportCreateUpdate.Dates = Convert.ToDateTime(SelectedDate);
            if (reportCreateUpdate != null)
            {
                await viewModel.GetPromoterAttendanceReport(reportCreateUpdate);
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

        async void txtStores_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            List<int?> SelectedAccountsList = new List<int?>();
            if (SelectedAccounts != null && SelectedAccounts.Count > 0)
            {
                foreach (var item in SelectedAccounts)
                {
                    SelectedAccountsList.Add(item.ID);
                }
            }

            List<int?> SelectedCountryList = new List<int?>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryList.Add(item.ID);
                }
            }

            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();
            if ((SelectedCountryList != null && SelectedCountryList.Count > 0) && (SelectedAccountsList != null && SelectedAccountsList.Count > 0))
            {
                reportCreateUpdateRequest.CountryIds = SelectedCountryList;
                reportCreateUpdateRequest.AccountIds = SelectedAccountsList;
            }

            if (reportCreateUpdateRequest != null)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(8, reportCreateUpdateRequest, 1));
            }
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            List<int?> SelectedAccountsList = new List<int?>();
            if (SelectedAccounts != null && SelectedAccounts.Count > 0)
            {
                foreach (var item in SelectedAccounts)
                {
                    SelectedAccountsList.Add(item.ID);
                }
            }

            List<int?> SelectedCountryList = new List<int?>();
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                foreach (var item in SelectedCountries)
                {
                    SelectedCountryList.Add(item.ID);
                }
            }

            ReportCreateUpdateRequest reportCreateUpdateRequest = new ReportCreateUpdateRequest();
            if ((SelectedCountryList != null && SelectedCountryList.Count > 0) && (SelectedAccountsList != null && SelectedAccountsList.Count > 0))
            {
                reportCreateUpdateRequest.CountryIds = SelectedCountryList;
                reportCreateUpdateRequest.AccountIds = SelectedAccountsList;
            }

            if (reportCreateUpdateRequest != null)
            {
                await Navigation.PushPopupAsync(new MultiselectPopupView(8, reportCreateUpdateRequest, 1));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.ViewModels.Attendance;
using Retail.ViewModels.Dashboard;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.Attendance
{
    public partial class AttendanceSummaries : ContentPage
    {
        AttendanceSummariesViewModel viewModel { get; set; }
        ObservableCollection<AssignedLocations> lstAssignedLocations = new ObservableCollection<AssignedLocations>();
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }
        public List<int?> SelectedCountryIds { get; set; }

        public AttendanceSummaries()
        {
            InitializeComponent();
            BindingContext = viewModel = new AttendanceSummariesViewModel(Navigation);
            
            //viewModel.GetPromoterList();
            rbMonthly.IsChecked = true;
        }

      

        void rbDaily_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (rbDaily.IsChecked)
            {
                DailyAttendace.IsVisible = true;
                PromoterAttendance.IsVisible = false;
            }
            else
            {
                DailyAttendace.IsVisible = false;
                PromoterAttendance.IsVisible = true;
            }
        }

        void rbMonthly_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (rbMonthly.IsChecked)
            {
                DailyAttendace.IsVisible = false;
                PromoterAttendance.IsVisible = true;
               
            }
            else
            {
                DailyAttendace.IsVisible = true;
                PromoterAttendance.IsVisible = false;               
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedCountryListsAttendance", async (selectedItems) =>
            {
                List<int?> SelectedCountryIds = new List<int?>();
                string CountryNames = "";
                SelectedCountries = new ObservableCollection<SelectedCountries>();
                if (selectedItems != null && selectedItems.Count > 0)
                {
                    foreach (var item in selectedItems)
                    {
                        if (item.ID != 0)
                        {
                            SelectedCountries.Add(new SelectedCountries { ID = item.ID, Name = item.Name });
                            SelectedCountryIds.Add(item.ID);
                            CountryNames = CountryNames + item.Name + ",";
                        }
                    }
                    txtCountry.Text = CountryNames.TrimEnd(',');
                    //pickerPromoters.PickerItemsSource.Clear();
                    //pickerPromoters.SelectedItem = new DropDownModel();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.GetPromoterList(SelectedCountryIds);
                    });
                }

            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedCountries", async (selectedItems) =>
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

                txtCountryIds.Text = CountryNames.TrimEnd(',');

            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedLocationLists", async (selectedItems) =>
            {
                string Locations = "";
                List<int> LocationIds = new List<int>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        lstAssignedLocations = new ObservableCollection<AssignedLocations>();
                        lstAssignedLocations.Add(new AssignedLocations { ID = item.ID, Name = item.Name });
                        LocationIds.Add(item.ID);
                        Locations = Locations + item.Name + ",";
                    }                    
                }
                txtLocations.Text = Locations.TrimEnd(',');
                if (txtLocations.Text != null && txtLocations.Text != string.Empty)
                {
                    await viewModel.GetDailyAttendanceHistoryByLocationIds(LocationIds);
                }
            });
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (CommonAttribute.CustomeProfile?.PersonId != null)
            {
                if (viewModel.NetworkAvailable)
                {
                    SelectedCountryIds = new List<int?>();
                    if (SelectedCountries != null && SelectedCountries.Count > 0)
                    {
                        foreach (var item in SelectedCountries)
                        {
                            SelectedCountryIds.Add(item.ID);
                        }
                    }
                    await Navigation.PushPopupAsync(new MultiselectPopupView(4, SelectedCountryIds, 0));
                }
                else
                {
                    viewModel.showToasterNoNetwork();
                }
            }
        }

        async void txtLocations_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            
            if (CommonAttribute.CustomeProfile?.PersonId != null)
            {
                if(viewModel.NetworkAvailable)
                {
                    SelectedCountryIds = new List<int?>();
                    if (SelectedCountries != null && SelectedCountries.Count > 0)
                    {
                        foreach(var item in SelectedCountries)
                        {
                            SelectedCountryIds.Add(item.ID);
                        }
                    }
                    await Navigation.PushPopupAsync(new MultiselectPopupView(4, SelectedCountryIds, 0));
                }
                else
                {
                    viewModel.showToasterNoNetwork();
                }
            }
        }

        async void txtCountry_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(11, null));
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(11, null));
        }

        async void txtCountryIds_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(10, null,"Daily"));
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(10, null, "Daily"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Retail.Models;
using Retail.ViewModels.SalesTarget;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.SalesTargetViews
{
    public partial class TargetsOverview : ContentPage
    {
        public TargetsOverviewViewModel viewModel { get; set; }
        ObservableCollection<SelectedItems> Locations { get; set; }
        ObservableCollection<Cities> CityList { get; set; }
        public ObservableCollection<SelectedCountries> SelectedCountries { get; set; }

        public TargetsOverview()
        {
            InitializeComponent();
            BindingContext = viewModel = new TargetsOverviewViewModel(Navigation, 0);
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
        private async void txtCity_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                List<int> SelectedCountry = new List<int>();
                foreach(var item in SelectedCountries)
                {
                    SelectedCountry.Add(item.ID);
                }
                await Navigation.PushPopupAsync(new MultiselectStorePopup(2, SelectedCountry));
            }
        }

        private async void txtStore_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectStorePopup(1, CityList));                       
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
                
            });
            MessagingCenter.Subscribe<List<SelectedItems>>(this, "SelectedStoreLists",(selectedItems)=>
            {
                Locations = new ObservableCollection<SelectedItems>();
                string SelectedLocations = "";
                
                    foreach (var item in selectedItems)
                    {
                        if (item.ID != 0)
                        {
                            Locations.Add(new SelectedItems { ID = item.ID, Name = item.Name });
                            SelectedLocations = SelectedLocations + item.Name + ",";
                        }                                              
                    }
                    txtLocationNames.Text = SelectedLocations.TrimEnd(',');
                
            });

            MessagingCenter.Subscribe<List<SelectedItems>>(this, "SelectedCityLists", (selectedItems) =>
            {
                CityList = new ObservableCollection<Cities>();
                string CityNames = "";
               
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        CityList.Add(new Cities { ID = item.ID, Name = item.Name });
                        CityNames = CityNames + item.Name + ",";
                    }
                                           
                }
                txtCityNames.Text = CityNames.TrimEnd(',');
               
            });

        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (txtCityNames.Text == string.Empty || txtCityNames.Text == null)
            {
                await DisplayAlert("Alert", "Please select city.", "Ok");
                return;
            }
            else if (txtLocationNames.Text == string.Empty || txtLocationNames.Text == null)
            {
                await DisplayAlert("Alert", "Please select store.", "Ok");
                return;
            }   
            try
            {
                if (CityList != null && Locations != null)
                    await viewModel.GetTargetSummary(CityList, Locations);               
            }
            catch(Exception ex)
            { }
        }

        //City
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {            
            if (SelectedCountries != null && SelectedCountries.Count > 0)
            {
                List<int> SelectedCountry = new List<int>();
                foreach (var item in SelectedCountries)
                {
                    SelectedCountry.Add(item.ID);
                }
                await Navigation.PushPopupAsync(new MultiselectStorePopup(2, SelectedCountry));
            }
        }

        //Store
        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectStorePopup(1, CityList));
        }

        async void txtCountry_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new MultiselectPopupView(5, null));
        }
    }
}

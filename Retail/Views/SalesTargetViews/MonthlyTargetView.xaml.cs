using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Controls;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.SalesTarget;
using Retail.Views.CommonPages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.SalesTargetViews
{
    public partial class MonthlyTargetView : ContentPage
    {
        public MonthlyTargetViewModel viewModel { get; set; }
        public string SelectedMonthDate { get; set; }
        public ObservableCollection<SelectedItems> selectedItems { get; set; }
        public ToolbarItem toolbarItem { get; set; }

        public MonthlyTargetView()
        {
            InitializeComponent();
            progressBar.IsVisible = true;
            
            BindingContext = viewModel= new MonthlyTargetViewModel(Navigation);
            Locations = new ObservableCollection<EntityKeyValueResponse>();
            
            var firstDayOfMonth = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1);
            SelectedMonthDate = firstDayOfMonth.ToString();

            var mindate = new DateTime(2000, 01, 01);
            var maxdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);

            monthyearpicker.MinDate = mindate;
            monthyearpicker.MaxDate = maxdate;
            monthyearpicker.Date = DateTime.Now;
            Device.BeginInvokeOnMainThread(async () =>
            {
                DateTime MonthYear = monthyearpicker.Date;

               await  GetLocationbyPersonId();
               
                //if (Locations != null && Locations.Count != 0)
                //{
                //    viewModel.IsBusy = true;
                //    viewModel.SelectedMY = monthyearpicker.Date;
                //    await viewModel.GetPromoterTargetSummary(Locations, MonthYear);
                //    viewModel.IsBusy = false;
                //}

            });
            DateTime MonthYear = monthyearpicker.Date;
            toolbarItem = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("refresh"),
                Order = ToolbarItemOrder.Primary,
                Command = viewModel.RefreshSalesTarget,
                Priority = 0
            };

            this.ToolbarItems.Add(toolbarItem);

        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            Xamarin.Forms.Label label = sender as Xamarin.Forms.Label;
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

        private void SubCatCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void txtLocationNames_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //if(!string.IsNullOrEmpty(txtLocationNames.Text))
            //{
            //    List<string> SelectedStoresList = txtLocationNames.Text.Split(',').ToList();

            //    await Navigation.PushPopupAsync(new MultiselectPopupView(SelectedStoresList, (int)CommonAttribute.CustomeProfile?.PersonId, 3));
            //}
            //else
            //{
                //await Navigation.PushPopupAsync(new MultiselectPopupView((int)CommonAttribute.CustomeProfile?.PersonId, 3));
                await Navigation.PushPopupAsync(new MultiselectPopupView((int)CommonAttribute.CustomeProfile?.PersonId, 13, 1));

            //}
        }

        async void MenuItem1_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SalesDataEntry());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetLocationbyPersonId();
            MessagingCenter.Unsubscribe<ObservableCollection<SelectedItems>>(this, "SelectedLocationListsbyPerId");
            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedLocationListsbyPerId", async (selectedItems) =>
            {
                viewModel.IsBusy = true;
                string CityNames = "";
                Locations = new ObservableCollection<EntityKeyValueResponse>();
                foreach (var item in selectedItems)
                {
                    if (item.ID != 0)
                    {
                        Locations.Add(new EntityKeyValueResponse
                        { Id = item.ID, Name = item.Name });

                        CityNames = CityNames + item.Name + ",";
                    }
                                      
                }
                txtLocationNames.Text = CityNames.TrimEnd(',');

                IsBusy = true;
                viewModel.SelectedMY = monthyearpicker.Date;
                DateTime MonthYear = monthyearpicker.Date;
                if (Locations != null && Locations.Count != 0)
                    await viewModel.GetPromoterTargetSummary(Locations, MonthYear);
                else
                    await DisplayAlert("", "Please select store.", "Ok");
            });

            //DateTime MonthYear = monthyearpicker.Date;
            
            //if (Locations != null && Locations.Count != 0)
            //{
            //    viewModel.IsBusy = true;
            //    viewModel.SelectedMY = monthyearpicker.Date;
            //    await viewModel.GetPromoterTargetSummary(Locations, MonthYear);
            //    viewModel.IsBusy = false;
            //}
        }

        public bool NetworkAvailable { get { return Retail.Hepler.Extensions.CheckNetwrokAvailability(); } }

        public async Task GetLocationbyPersonId()
        {

            try
            {
                IsBusy = true;
                if (NetworkAvailable)
                {
                    string StoreNames = "";
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    var ResultLocation = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (ResultLocation != null && ResultLocation.Count > 0)
                    {
                        selectedItems = new ObservableCollection<SelectedItems>();
                        Locations = new ObservableCollection<EntityKeyValueResponse>();
                        foreach (var item in ResultLocation)
                        {
                            if (item.Id != 0)
                            {
                                Locations.Add(new EntityKeyValueResponse { Id = item.Id, Name = item.Name });
                                selectedItems.Add(new SelectedItems { ID = item.Id, Name = item.Name });

                                StoreNames = StoreNames + item.Name + ",";
                            }
                        }

                        txtLocationNames.Text = StoreNames.TrimEnd(',');
                        viewModel.selectedLocations = selectedItems;
                        //  MessagingCenter.Send<ObservableCollection<SelectedItems>>(selectedItems, "SelectedLocationListsbyPerId");

                   //  await  viewModel.SelectedLocationListsbyPerId();
                        progressBar.IsVisible = false;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }


        private List<EntityKeyValueResponse> _Location;
        public List<EntityKeyValueResponse> Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("Location");
            }
        }

        private ObservableCollection<EntityKeyValueResponse> _Locations;
        public ObservableCollection<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }
        public DateTime CureentMonthYear { get; set; }
        async void monthyearpicker_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DateTime MonthYear = monthyearpicker.Date;

            //if (Locations != null && Locations.Count != 0)
            //{
            //    if (CureentMonthYear != MonthYear)
            //    {
            //        CureentMonthYear = MonthYear;
            //        viewModel.IsBusy = true;
            //        viewModel.SelectedMY = monthyearpicker.Date;
            //        await viewModel.GetPromoterTargetSummary(Locations, MonthYear);
            //        viewModel.IsBusy = false;
            //    }
            //}
            progressBar.IsVisible = false;
        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
           
            //await Navigation.PushPopupAsync(new MultiselectPopupView((int)CommonAttribute.CustomeProfile.PersonId, 3));
            MultiselectPopupView multiselectPopupView=new  MultiselectPopupView((int)CommonAttribute.CustomeProfile?.PersonId, 13, 1);
            multiselectPopupView.ProcessCompleted += MultiselectPopupView_ProcessCompleted;
            await Navigation.PushPopupAsync(multiselectPopupView);

        }

        private void MultiselectPopupView_ProcessCompleted(object sender, ObservableCollection<SelectedItems> e)
        {

            progressBar.IsVisible = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                string CityNames = "";
                Locations = new ObservableCollection<EntityKeyValueResponse>();
                foreach (var item in e)
                {
                    if (item.ID != 0)
                    {
                        Locations.Add(new EntityKeyValueResponse
                        { Id = item.ID, Name = item.Name });

                        CityNames = CityNames + item.Name + ",";
                    }

                }
                txtLocationNames.Text = CityNames.TrimEnd(',');
                viewModel.selectedLocations = e;
                //   await progressBar.ProgressTo(0.60, 500, Easing.Linear);
                IsBusy = true;
                viewModel.SelectedMY = monthyearpicker.Date;
                DateTime MonthYear = monthyearpicker.Date;

                // await viewModel.SelectedLocationListsbyPerId(e);
                await viewModel.SelectedLocationListsbyPerId();
                IsBusy = false;
                progressBar.IsVisible = false;
                // await progressBar.ProgressTo(1, 500, Easing.SpringOut);
                // progressBar.IsVisible = false;
            });
          
            
           
        }

        void   btnApply_Clicked(System.Object sender, System.EventArgs e)
        {
            progressBar.IsVisible = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.SelectedLocationListsbyPerId();
                progressBar.IsVisible = false;
            });
        }
    }
}

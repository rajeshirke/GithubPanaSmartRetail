using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.SupervisorFlow;
using Retail.Views.CommonPages;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.Views.SupervisorFlow
{
    public partial class PromoterTargetsView : ContentPage
    {
        PromoterTargetsViewModel viewModel { get; set; }
        int PersonIds; int LocationId;
        ObservableCollection<SelectedItems> Promoters = new ObservableCollection<SelectedItems>();
        ObservableCollection<Cities> Cities = new ObservableCollection<Cities>();
        public List<int> LocationIds { get; set; }

        public PromoterTargetsView(PersonLocationTarget personLocationTarget)
        {
            InitializeComponent();            
           
            Cities = new ObservableCollection<Cities>();
            Promoters = new ObservableCollection<SelectedItems>();
            PersonIds = (int)personLocationTarget.PersonId;
            LocationId = (int)personLocationTarget.LocationID;
            if (personLocationTarget.PersonName!=string.Empty)
            {
                txtPromoters.Text = personLocationTarget.PersonName;
                Promoters.Add(new SelectedItems { ID = (int)personLocationTarget.PersonId, Name = personLocationTarget.PersonName });
            }
            if (personLocationTarget.LocationStoreName != string.Empty)
            {
                txtLocationNames.Text = personLocationTarget.LocationStoreName;
                Cities.Add(new Cities { ID =(int) personLocationTarget.LocationID, Name = personLocationTarget.LocationStoreName });
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.GetPromoterTargetSummary(Cities, Promoters, DateTime.Today);
            });

            BindingContext = viewModel = new PromoterTargetsViewModel(Navigation);
            
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

        private async void txtPromoters_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            txtPromoters.Text = "";
            LocationIds = new List<int>();
            foreach(var item in Cities)
            {
                 LocationIds.Add(item.ID); 
            }
            if (txtLocationNames.Text != string.Empty && txtLocationNames.Text != null)
                await Navigation.PushPopupAsync(new MultiselectPopupView(PersonIds, LocationIds, 1));
            else
                await DisplayAlert("", "Please select location first.", "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedPersonLists", async (selectedItems) =>
            {
                Promoters = new ObservableCollection<SelectedItems>();
                string Persons = ""; string ids = "";

                foreach (var item in selectedItems)
                {                    
                    Promoters.Add(new SelectedItems { ID = item.ID, Name = item.Name });
                    Persons = Persons + item.Name + ",";
                    ids = ids + item.ID + ",";
                }
                txtPromoters.Text = Persons.TrimEnd(',');
                try
                {
                    IsBusy = true;
                    if (Cities == null && Cities.Count == 0)
                    {
                        await DisplayAlert("Alert", "Please select city.", "Ok");
                        return;
                    }
                    else if (Promoters == null && Promoters.Count == 0)
                    {
                        await DisplayAlert("Alert", "Please select store.", "Ok");
                        return;
                    }
                    DateTime MonthYear = monthyearpicker.Date;
                    await viewModel.GetPromoterTargetSummary(Cities, Promoters, MonthYear);

                }
                catch (Exception ex)
                { }
            });

            MessagingCenter.Subscribe<ObservableCollection<SelectedItems>>(this, "SelectedLocationLists", async (selectedItems) =>
            {

                string CityNames = "";
                Cities = new ObservableCollection<Cities>();
                foreach (var item in selectedItems)
                {                    
                    Cities.Add(new Cities { ID = item.ID, Name = item.Name });
                    CityNames = CityNames + item.Name + ",";
                }
                txtLocationNames.Text = CityNames.TrimEnd(',');


            });

            MessagingCenter.Subscribe<string>(this, "MonthYearChangeDate", async (selectedItems) =>
            {
                string selecteddate = selectedItems;
                try
                {
                    viewModel.SelectedMandY = Convert.ToDateTime(selecteddate);
                    await viewModel.GetPromoterTargetSummary(Cities, Promoters, Convert.ToDateTime(selecteddate));
                }
                catch (Exception ex)
                { }

            });
         }

        private async void txtLocationNames_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            LocationIds = new List<int>();
            LocationIds.Add(LocationId);
            await Navigation.PushPopupAsync(new MultiselectPopupView(PersonIds,LocationIds,2));
        }

        //promoter
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            txtPromoters.Text = "";
            LocationIds = new List<int>();
            foreach (var item in Cities)
            {
                LocationIds.Add(item.ID);
            }
            if (txtLocationNames.Text != string.Empty && txtLocationNames.Text != null)
                await Navigation.PushPopupAsync(new MultiselectPopupView(PersonIds, LocationIds, 1));
            else
                await DisplayAlert("", "Please select location first.", "Ok");
        }

        //store
        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            LocationIds = new List<int>();
            LocationIds.Add(LocationId);
            await Navigation.PushPopupAsync(new MultiselectPopupView(PersonIds, LocationIds, 2));
        }
    }
}

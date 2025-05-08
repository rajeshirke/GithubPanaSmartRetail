using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits;
using Xamarin.Forms;

namespace Retail.Views.MyVisits
{
    public partial class PlannedVisitsView : ContentPage
    {
        public PlannedVisitsViewModel visitsViewModel { get; set; }
        public string SearchStoreName { get; set; }

        public PlannedVisitsView()
        {
            InitializeComponent();
            BindingContext = visitsViewModel = new PlannedVisitsViewModel(Navigation);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // on back button click
            MessagingCenter.Subscribe<string>(this, "SupPromVisitBack", async (item) =>
            {
                if (item != null)
                {
                    //viewModel._VisitLocationID = (int)item.VisitScheduleLocationId;
                    await visitsViewModel.GetScheduledVisits();

                    //search store name
                    if (SearchStoreName != null && SearchStoreName != string.Empty)
                    {
                        visitsViewModel.SearchLocationCommand.Execute(SearchStoreName);
                    }
                }
            });


        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        public async void VisitDate_ItemTapped(System.Object sender, System.EventArgs e)
        {
            //var dp = sender as DatePicker;
            //visitsViewModel.SelectedDate = dp.Date;
            //if (visitsViewModel.SelectedDate != null)
            //{
            //    await visitsViewModel.GetScheduledVisits();
            //}
        }

        async void LocationName_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                SearchStoreName = e.NewTextValue;
                visitsViewModel.SearchLocationCommand.Execute(e.NewTextValue);
            }
            else
            {
                //await visitsViewModel.GetScheduledVisits();
                SearchStoreName = "";
                var Result = visitsViewModel.lstVisitScheduleLocationResponses;
                if (Result != null && Result.Count > 0)
                {
                    visitsViewModel.VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(Result);
                }
            }
        }
    }
}
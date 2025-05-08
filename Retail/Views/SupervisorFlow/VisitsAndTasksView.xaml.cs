using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.SupervisorFlow;
using Xamarin.Forms;

namespace Retail.Views.SupervisorFlow
{
    public partial class VisitsAndTasksView : ContentPage
    {        
        public VisitsandTasksViewModel viewModel;

        public VisitsAndTasksView()
        {
            InitializeComponent();
            BindingContext = viewModel = new VisitsandTasksViewModel(Navigation);
            viewModel.ToolBarITem += VisitTasksModel_ToolBarITem;

        }

        private void VisitTasksModel_ToolBarITem(object sender, EventArgs e)
        {
            try
            {
                ToolbarItems.Clear();
                var e1 = (ToolBarEventArgs)e;
                if (e1.IconImage)
                {
                    ToolbarItems.Add(new ToolbarItem()
                    {
                        IconImageSource = "noteadd.png",
                        Text = " ",
                        Command = viewModel.StoreSelectCommand
                    });
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, int>(this, "SelectedStore", async (obj, store) =>
            {

                //if (viewModel.VisitScheduleLocationResponses != null && viewModel.VisitScheduleLocationResponses.Count != 0)
                //{
                //    foreach (var item in viewModel.VisitScheduleLocationResponses)
                //    {
                //        if (item.LocationId != store)
                            await viewModel.GetScheduledVisits();
                //        else
                //          await DisplayAlert("", "Location already added", "Ok");
                //    }
                //}
            });

            // on back button click
            MessagingCenter.Subscribe<string>(this, "SupPromVisitBack", async (item) =>
            {
                if (item != null)
                {
                    //viewModel._VisitLocationID = (int)item.VisitScheduleLocationId;
                    //viewModel.loadDates(DateTime.Today);
                    await viewModel.GetScheduledVisits();
                    MessagingCenter.Unsubscribe<string>("", "SupPromVisitBack");
                }
            });

        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");
            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        //void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        //{
        //    viewModel.FinalSubmitSupWorkflowOffline();
        //}
    }

    public class ToolBarEventArgs : EventArgs
    {
        public bool IconImage { get; set; }
    }

}

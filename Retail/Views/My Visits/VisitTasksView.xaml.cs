using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits;
using Retail.Views.SupervisorFlow;
using Xamarin.Forms;

namespace Retail.Views.MyVisits
{
    public partial class VisitTasksView : ContentPage
    {
        public VisitTasksViewModel viewModel { get; set; }

        public VisitTasksView(string StoreName,string StoreAddress,double Distance,long VisitLocationID, bool CheckList)
        {
            InitializeComponent();
            BindingContext = viewModel= new VisitTasksViewModel(Navigation, StoreName, StoreAddress, Distance, VisitLocationID, CheckList);           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // on back button click
            MessagingCenter.Subscribe<VisitLocationTaskResponse> (this, "TaskSummaryBack", async (visitLocationTaskResponse) =>
            {
                if (visitLocationTaskResponse != null)
                {
                    viewModel._VisitLocationID = (int)visitLocationTaskResponse.VisitScheduleLocationId;
                    await viewModel.GetTaskDetails();
                }
            });
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();

        //    if (lstRoleIds.Contains((int)PersonRoleEnum.Promoter))
        //    {
        //        Navigation.PushAsync(new PlannedVisitsView());
        //    }
        //    else
        //    {
        //        Navigation.PushAsync(new VisitsAndTasksView());
        //    }
        //    return base.OnBackButtonPressed();

        //}

        protected override bool OnBackButtonPressed()
        {
            try
            {
                MessagingCenter.Send<string>("", "SupPromVisitBack");
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                //IsBusy = false;
            }
            return true;
            //return base.OnBackButtonPressed();
        }


        async void Task_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                viewModel.SearchTaskCommand.Execute(e.NewTextValue);
            }
            else
            {
                //await viewModel.GetTaskDetails();
                var Result = viewModel.SearchVisitLocationTaskResponses;
                viewModel.VisitLocationTaskResponses = new ObservableCollection<VisitLocationTaskResponse>(Result);
            }
                
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await viewModel.FinalSubmitSupWorkflowOffline();
        }
    }
}

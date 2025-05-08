using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits;
using Retail.ViewModels.MyVisits.TaskSummary;
using Xamarin.Forms;

namespace Retail.Views.MyVisits.TaskSummary
{
    public partial class TakeDisplayVideo : ContentPage
    {
        TaskSummaryViewModel taskSummaryViewModel { get; set; }
        public TakeVideoVideoModel takeVideoViewModel { get; set; }
        public TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 { get; set; }


        public TakeDisplayVideo(ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles1,
            TaskQuestionAnswerToSubmitResponse _TaskQuestionAnswer1)
        {
            InitializeComponent();

            TaskQuestionAnswer1 = _TaskQuestionAnswer1;
            BindingContext = takeVideoViewModel = new TakeVideoVideoModel(Navigation, AnswerUploadedFiles1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                MessagingCenter.Send<ObservableCollection<AnswerUploadedFileResponse>>(takeVideoViewModel.AnswerUploadedFiles, "AnswerUploadedFileVideo");

                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        void TapGestureRecognizer_Tapped_Cancel(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

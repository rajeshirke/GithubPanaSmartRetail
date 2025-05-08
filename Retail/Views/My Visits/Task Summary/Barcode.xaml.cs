using System;
using System.Collections.Generic;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits.TaskSummary;
using Xamarin.Forms;

namespace Retail.Views.MyVisits.TaskSummary
{
    public partial class Barcode : ContentPage
    {
        public BarCodeViewModel BarCodeViewModel { get; set; }
        public TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 { get; set; }


        public Barcode(TaskQuestionAnswerToSubmitResponse _TaskQuestionAnswer1)
        {
            InitializeComponent();

            TaskQuestionAnswer1 = _TaskQuestionAnswer1;
            BindingContext = BarCodeViewModel = new BarCodeViewModel(Navigation, TaskQuestionAnswer1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                //MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(BarCodeViewModel.TaskQuestionAnswer, "AnsweredBarcode");
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

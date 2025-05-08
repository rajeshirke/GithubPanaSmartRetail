using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MarketIntelligence;
using Retail.ViewModels.MarketIntelligence.Questionnaires;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence.Questionnaires
{
    public partial class TakeDisplayVideo : ContentPage
    {
        public TakeVideoVideoModel takeVideoViewModel { get; set; }
        public MarketIntelQuestionAnswerToSubmitResponse TaskQuestionAnswer1 { get; set; }


        public TakeDisplayVideo(ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles1,
            MarketIntelQuestionAnswerToSubmitResponse _TaskQuestionAnswer1)
        {
            InitializeComponent();

            TaskQuestionAnswer1 = _TaskQuestionAnswer1;
            BindingContext = takeVideoViewModel = new TakeVideoVideoModel(Navigation, AnswerUploadedFiles1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<MarketIntelQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                MessagingCenter.Send<ObservableCollection<AnswerUploadedFileResponse>>(takeVideoViewModel.AnswerUploadedFiles, "MarketAnswerUploadedFileVideo");

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

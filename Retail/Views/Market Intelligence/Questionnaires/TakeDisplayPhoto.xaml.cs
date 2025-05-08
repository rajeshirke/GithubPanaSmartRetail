using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MarketIntelligence;
using Retail.ViewModels.MarketIntelligence.Questionnaires;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence.Questionnaires
{
    public partial class TakeDisplayPhoto : ContentPage
    {
        public TakePhotoViewModel takePhotoViewModel { get; set; }
        public MarketIntelQuestionAnswerToSubmitResponse TaskQuestionAnswer1 { get; set; }

        public TakeDisplayPhoto(ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles1,
            MarketIntelQuestionAnswerToSubmitResponse _TaskQuestionAnswer1)
        {
            InitializeComponent();

            TaskQuestionAnswer1 = _TaskQuestionAnswer1;
            BindingContext = takePhotoViewModel = new TakePhotoViewModel(Navigation, AnswerUploadedFiles1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<MarketIntelQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                MessagingCenter.Send<ObservableCollection<AnswerUploadedFileResponse>>(takePhotoViewModel.AnswerUploadedFiles, "MarketAnswerUploadedFilePhoto");

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

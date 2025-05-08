using System;
using System.Collections.Generic;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MarketIntelligence.Questionnaires;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence.Questionnaires
{
    public partial class QRCode : ContentPage
    {
        public QRCodeViewModel QRCodeViewModel { get; set; }

        public QRCode(MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer1)
        {
            InitializeComponent();

            BindingContext = QRCodeViewModel = new QRCodeViewModel(Navigation, MarketIntelQuestionAnswer1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<MarketIntelQuestionAnswerToSubmitResponse>(QRCodeViewModel.MarketIntelQuestionAnswer, "MarketAnsweredQrcode");
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

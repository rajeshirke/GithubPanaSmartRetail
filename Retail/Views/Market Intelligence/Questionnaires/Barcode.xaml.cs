using System;
using System.Collections.Generic;
using System.Diagnostics;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MarketIntelligence.Questionnaires;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence.Questionnaires
{
    public partial class Barcode : ContentPage
    {
        public BarCodeViewModel BarCodeViewModel { get; set; }

        public Barcode(MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer1)
        {
            InitializeComponent();

            BindingContext = BarCodeViewModel = new BarCodeViewModel(Navigation, MarketIntelQuestionAnswer1);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<MarketIntelQuestionAnswerToSubmitResponse>(BarCodeViewModel.MarketIntelQuestionAnswer, "MarketAnsweredBarcode");
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

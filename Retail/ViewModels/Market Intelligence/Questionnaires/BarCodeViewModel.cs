using System;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence.Questionnaires
{
    public class BarCodeViewModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }

        public BarCodeViewModel(INavigation navigation, MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer1) : base(navigation)
        {

            IsBusy = true;
            MarketIntelQuestionAnswer = MarketIntelQuestionAnswer1;

            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
            });

            ImageButtonCommand = new Command(ImageButtonClick);
        }

        private async void ImageButtonClick(object obj)
        {
            try
            {
                MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer1 = new MarketIntelQuestionAnswerToSubmitResponse();
                MarketIntelQuestionAnswer1 = MarketIntelQuestionAnswer;

                var barcode = await Retail.Hepler.Extensions.QrScanning();
                if (MarketIntelQuestionAnswer1 != null && barcode != null)
                {
                    MarketIntelQuestionAnswer1.AnswerBarcode = barcode;
                    MarketIntelQuestionAnswer = MarketIntelQuestionAnswer1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        MarketIntelQuestionAnswerToSubmitResponse _TaskQuestionAnswer;
        public MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer
        {
            get { return _TaskQuestionAnswer; }
            set {
                SetProperty(ref _TaskQuestionAnswer, value);
                OnPropertyChanged(nameof(AnswerBarcode));
            }
        }

        public string _AnswerBarcode { get; set; }
        public string AnswerBarcode
        {
            get
            {
                if (MarketIntelQuestionAnswer != null)
                {
                    _AnswerBarcode = MarketIntelQuestionAnswer.AnswerBarcode;
                }
                return _AnswerBarcode;
            }
        }
    }
}

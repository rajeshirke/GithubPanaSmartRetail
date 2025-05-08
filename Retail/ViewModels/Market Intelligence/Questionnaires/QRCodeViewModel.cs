using System;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence.Questionnaires
{
    public class QRCodeViewModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }

        public QRCodeViewModel(INavigation navigation, MarketIntelQuestionAnswerToSubmitResponse MarketIntelQuestionAnswer1) : base(navigation)
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

                var qrcode = await Retail.Hepler.Extensions.QrScanning();
                if (MarketIntelQuestionAnswer1 != null && qrcode != null)
                {
                    MarketIntelQuestionAnswer1.AnswerQrcode = qrcode;
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
                OnPropertyChanged(nameof(AnswerQrcode));
            }
        }

        public string _AnswerQrcode { get; set; }
        public string AnswerQrcode
        {
            get {
                if (MarketIntelQuestionAnswer != null)
                {
                    _AnswerQrcode = MarketIntelQuestionAnswer.AnswerQrcode;
                }
                return _AnswerQrcode;
            }
        }
    }
}

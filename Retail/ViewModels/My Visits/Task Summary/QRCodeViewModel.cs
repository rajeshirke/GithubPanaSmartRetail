using System;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits.TaskSummary
{
    public class QRCodeViewModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }

        public QRCodeViewModel(INavigation navigation, TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1) : base(navigation)
        {

            IsBusy = true;
            TaskQuestionAnswer = TaskQuestionAnswer1;

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
                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = new TaskQuestionAnswerToSubmitResponse();
                TaskQuestionAnswer1 = TaskQuestionAnswer;

                var qrcode = await Retail.Hepler.Extensions.QrScanning();
                if (TaskQuestionAnswer1 != null && qrcode != null)
                {
                    TaskQuestionAnswer1.AnswerQrcode = qrcode;
                    TaskQuestionAnswer = TaskQuestionAnswer1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        TaskQuestionAnswerToSubmitResponse _TaskQuestionAnswer;
        public TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer
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
                if (TaskQuestionAnswer != null)
                {
                    _AnswerQrcode = TaskQuestionAnswer.AnswerQrcode;
                }
                return _AnswerQrcode;
            }
        }
    }
}

using System;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits.TaskSummary
{
    public class BarCodeViewModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }

        public BarCodeViewModel(INavigation navigation, TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1) : base(navigation)
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

                var barcode = await Retail.Hepler.Extensions.QrScanning();
                if (TaskQuestionAnswer1 != null && barcode != null)
                {
                    TaskQuestionAnswer1.AnswerBarcode = barcode;
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
                OnPropertyChanged(nameof(AnswerBarcode));
            }
        }

        public string _AnswerBarcode { get; set; }
        public string AnswerBarcode
        {
            get
            {
                if (TaskQuestionAnswer != null)
                {
                    _AnswerBarcode = TaskQuestionAnswer.AnswerBarcode;
                }
                return _AnswerBarcode;
            }
        }
    }
}

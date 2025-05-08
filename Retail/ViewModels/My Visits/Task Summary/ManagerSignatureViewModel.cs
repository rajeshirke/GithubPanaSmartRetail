using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using SignaturePad.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits.TaskSummary
{
    public class ManagerSignatureViewModel : BaseViewModel
    {
        public event EventHandler SignaturePadViewItem;
        public Command ImageButtonCommand { get; set; }

        public ManagerSignatureViewModel(INavigation navigation, ObservableCollection<AnswerUploadedFileResponse> answerUploadedFileResponses) : base(navigation)
        {
            IsBusy = true;

            if (answerUploadedFileResponses != null && answerUploadedFileResponses.Count > 0)
            {
                foreach (var product in answerUploadedFileResponses)
                {
                    //if (product.FileInfo?.FileTypeId == (int)FileTypeEnum.AnswerImageFile)
                    //{
                    //Display image online through API
                    //product.FileInfo.FileFullPath = CommonAttribute.ImageBaseUrl + product.FileInfo?.Path;

                    //display signature mainly in offline and online also
                    product.FileInfo.FileFullPath = product.FileInfo?.Base64StringImage;
                    //}
                }
            }

            AnswerUploadedFiles = answerUploadedFileResponses;

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsBusy = false;
            });

            ImageButtonCommand = new Command<string>(ImageButtonClick);
        }

        private async void ImageButtonClick(string submit)
        {
            if (IsSigBlank)
            {
                await ErrorDisplayAlert("Please provide customer signature.");
                return;
            }

            if (AnswerUploadedFiles != null)
            {
                AnswerUploadedFiles.Add(new AnswerUploadedFileResponse
                {
                    FileInfo = new FileInfoResponse
                    {
                        FileName = "png",
                        FileTypeId = (int)FileTypeEnum.SignatureImageFile,
                        Base64StringImage = signatureBase64string
                    }
                });
            }

            EventHandler handler = SignaturePadViewItem;
            handler?.Invoke(this, null);
        }

        public string signatureBase64string { get; set; }

        private ObservableCollection<AnswerUploadedFileResponse> _AnswerUploadedFiles;
        public ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles
        {
            get { return _AnswerUploadedFiles; }
            set
            {
                _AnswerUploadedFiles = value;
                OnPropertyChanged(nameof(AnswerUploadedFiles));
                OnPropertyChanged(nameof(FileInfos));
            }
        }

        public FileInfoResponse FileInfos
        {
            get {
                if (AnswerUploadedFiles != null
                    && AnswerUploadedFiles.Count > 0)
                {
                    return AnswerUploadedFiles[AnswerUploadedFiles.Count - 1].FileInfo;
                } else
                {
                    return new FileInfoResponse();
                }
            }
        }

        //IsBlank
        private bool _IsSigBlank;
        public bool IsSigBlank
        {
            get { return _IsSigBlank; }
            set
            {
                _IsSigBlank = value;
                OnPropertyChanged(nameof(IsSigBlank));
            }
        }
    }
}

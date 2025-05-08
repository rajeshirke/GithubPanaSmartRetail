using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits.TaskSummary
{
    public class TakeVideoVideoModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }
        public TakeVideoVideoModel(INavigation navigation, ObservableCollection<AnswerUploadedFileResponse> answerUploadedFileResponses) : base(navigation)
        {
            IsBusy = true;
            AnswerUploadedFiles = answerUploadedFileResponses;

            if (AnswerUploadedFiles != null && AnswerUploadedFiles.Count > 0)
            {
                foreach (var product in AnswerUploadedFiles)
                {
                    //if (product.FileInfo?.FileTypeId == (int)FileTypeEnum.AnswerVideoFile)
                    //{

                    //Display video online from API
                    //product.FileInfo.FileFullPath = CommonAttribute.ImageBaseUrl + product.FileInfo?.Path;

                    //Display video in offline and online
                    product.FileInfo.FileFullPath = product.FileInfo?.Base64StringImage;

                    //}
                }
            }

            Device.BeginInvokeOnMainThread(async () =>
            {

                IsBusy = false;
            });

            ImageButtonCommand = new Command(ImageButtonClick);
        }

        private async void ImageButtonClick(object obj)
        {
            FileData fileData = await TakeVideoAsync();
            if (fileData != null && !string.IsNullOrEmpty(fileData.FileName))
            {
                ImageSource DisplayVideo = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);
                //Application.Current.Properties["DisplayVideo"] = DisplayVideo;
                if (AnswerUploadedFiles != null)
                {
                    AnswerUploadedFiles.Add(new AnswerUploadedFileResponse
                    {
                        FileInfo = new FileInfoResponse
                        {
                            TempPath = newFile,
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.AnswerVideoFile,
                            Base64StringImage = fileData.string64baseData
                        }
                    });
                }
            }
        }

        private ObservableCollection<AnswerUploadedFileResponse> _AnswerUploadedFiles;
        public ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles

        {
            get { return _AnswerUploadedFiles; }
            set
            {
                _AnswerUploadedFiles = value;
                OnPropertyChanged(nameof(AnswerUploadedFiles));
            }
        }

        public string Videoaction { get; set; }

        public async Task<FileData> TakeVideoAsync()
        {
            FileData fileData = new FileData();

            try
            {
                GC.Collect();
                string action = await Application.Current.MainPage.DisplayActionSheet("Video Upload", "Cancel", null, "Take Video", "Gallery");
                FileResult video = null;

                if (action != null && action == "Gallery")
                {
                    Videoaction = action;
                    video = await MediaPicker.PickVideoAsync(new MediaPickerOptions() { });// CaptureVideoAsync();
                }
                else if (action != null && action == "Take Video")
                {
                    Videoaction = action;
                    if (!MediaPicker.IsCaptureSupported)
                    {
                        await ErrorDisplayAlert("Camera not supported");
                        return fileData;
                    }

                    video = await MediaPicker.CaptureVideoAsync();
                }

                if (video != null)
                {
                    Application.Current.Properties["Videoaction"] = Videoaction;
                    await Application.Current.SavePropertiesAsync();
                    return fileData = await LoadVideoAsync(video);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CaptureVideoAsync THREW: {ex.Message}");
                //return null;
            }

            return fileData;
        }
        async Task<FileData> LoadVideoAsync(FileResult video)
        {

            FileData fileData = new FileData();

            try
            {
                if (video == null)
                {
                    // VideoPath = null;
                    return null;
                }

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, video.FileName);
                fileData.FileType = video.ContentType;
                if (Device.RuntimePlatform == Device.Android)
                {
                    using (var stream = await video.OpenReadAsync())
                    {

                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        if (stream.Length > CommonAttribute.VideoLength)
                        {
                            await ErrorDisplayAlert("File length exceeds the limit, 10 mb");
                            return fileData;
                        }

                        string string64base = Retail.Hepler.Extensions.ConvertToBase64(stream);
                        fileData.string64baseData = string64base;
                    }
                }
                else
                {
                    using (var stream = await video.OpenReadAsync())
                    {
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        if (stream.Length > CommonAttribute.VideoLength)
                        {
                            await ErrorDisplayAlert("File length exceeds the limit, 10 mb");
                            return fileData;
                        }

                        var originalImageByteArray = new Byte[(int)stream.Length];

                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(originalImageByteArray, 0, (int)stream.Length);

                        //var resizer = DependencyService.Get<IImageResizer>();
                        //var resizedBytes = resizer.ResizeImage(originalImageByteArray, 400, 400);

                        string string64base = Convert.ToBase64String(originalImageByteArray);
                        fileData.string64baseData = string64base;
                    }




                }
                fileData.FileName = video.FileName;
            }
            catch (Exception ex)
            {
                //return null;
            }

            return fileData;
        }


        public ICommand DeleteVideoCommand
        {
            get
            {
                return new Command<AnswerUploadedFileResponse>((item) =>
                {
                    AnswerUploadedFiles.Remove(item);
                });
            }
        }
    }
}

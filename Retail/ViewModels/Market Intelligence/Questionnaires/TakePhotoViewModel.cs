using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence.Questionnaires
{
    public class TakePhotoViewModel : BaseViewModel
    {
        public Command ImageButtonCommand { get; set; }
        public TakePhotoViewModel(INavigation navigation, ObservableCollection<AnswerUploadedFileResponse> answerUploadedFileResponses) : base(navigation)
        {
            IsBusy = true;
            AnswerUploadedFiles = answerUploadedFileResponses;

            Device.BeginInvokeOnMainThread(async () =>
            {

                IsBusy = false;
            });

            ImageButtonCommand = new Command(ImageButtonClick);
        }

        private async void ImageButtonClick(object obj)
        {
            DisplayOrientation orientation = DeviceDisplay.MainDisplayInfo.Orientation;
            FileData fileData = await TakePhotoAsync();
            if (fileData != null && !string.IsNullOrEmpty(fileData.FileName))
            {
                ImageSource DisplayPhoto = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);
                //Application.Current.Properties["DisplayPhoto"] = DisplayPhoto;
                if(AnswerUploadedFiles != null)
                {
                    AnswerUploadedFiles.Add(new AnswerUploadedFileResponse {
                        FileInfo = new FileInfoResponse
                        {
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.AnswerImageFile,
                            Base64StringImage = fileData.string64baseData,
                            Rotation = ((orientation == DisplayOrientation.Landscape) || Photoaction == "Gallery") ? 0 : 90
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

        public string Photoaction { get; set; }

        public async Task<FileData> TakePhotoAsync()
        {
            FileData fileData = new FileData();
            try
            {
                GC.Collect();
                string action = await Application.Current.MainPage.DisplayActionSheet("Photo Upload", "Cancel", null, "Take Photo", "Gallery");
                FileResult photo = null;

                if (action != null && action == "Gallery")
                {
                    Photoaction = action;
                    photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() { });// CapturePhotoAsync();
                }
                else if (action != null && action == "Take Photo")
                {
                    Photoaction = action;
                    if (!MediaPicker.IsCaptureSupported)
                    {
                        await ErrorDisplayAlert("Camera not supported");
                        return fileData;
                    }

                    photo = await MediaPicker.CapturePhotoAsync();
                }

                if (photo != null)
                {
                    Application.Current.Properties["Photoaction"] = Photoaction;
                    await Application.Current.SavePropertiesAsync();
                    return fileData = await LoadPhotoAsync(photo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                //return null;
            }
            return fileData;
        }
        async Task<FileData> LoadPhotoAsync(FileResult photo)
        {

            FileData fileData = new FileData();

            try
            {
                if (photo == null)
                {
                    // PhotoPath = null;
                    return fileData;
                }

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                fileData.FileType = photo.ContentType;
                if (Device.RuntimePlatform == Device.Android)
                {
                    using (var stream = await photo.OpenReadAsync())
                    {

                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        string string64base = Retail.Hepler.Extensions.ConvertToBase64(stream);
                        fileData.string64baseData = string64base;
                    }
                }
                else
                {
                    using (var stream = await photo.OpenReadAsync())
                    {
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        var originalImageByteArray = new Byte[(int)stream.Length];

                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(originalImageByteArray, 0, (int)stream.Length);

                        var resizer = DependencyService.Get<IImageResizer>();
                        var resizedBytes = resizer.ResizeImage(originalImageByteArray, 400, 400);

                        string string64base = Convert.ToBase64String(resizedBytes);
                        fileData.string64baseData = string64base;
                    }




                }
                fileData.FileName = photo.FileName;
            }
            catch (Exception ex)
            {
                //return null;
            }

            return fileData;

        }


        public ICommand DeletePhotoCommand
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

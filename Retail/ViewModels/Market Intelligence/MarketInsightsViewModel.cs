using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Retail.Hepler;
using System.IO;
using Retail.DependencyServices;
using System.Collections.ObjectModel;
using Plugin.Media;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using System.Linq;
using System.Windows.Input;
using Retail.Views.CommonPages;
using System.Diagnostics;

namespace Retail.ViewModels.MarketIntelligence
{
    public class MarketInsightsViewModel : BaseViewModel
    {
        public ObservableCollection<ImageSource> Media { get; set; }
        public Command ImageButtonCommand { get; set; }

        public MarketInsightsViewModel(INavigation navigation):base(navigation)
        {
            IsBusy = true;
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsBusy = false;
            });

            ImageButtonCommand = new Command(ImageButtonClick);
            AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>();
            objMarketInsightRequest = new MarketInsightRequest();
            obMarketInsightFileRequest = new ObservableCollection<MarketInsightFileRequest>();
            lstMarketInsightFileRequest = new List<MarketInsightFileRequest>();
            obMarketInsightFileRequestVideo = new ObservableCollection<MarketInsightFileRequest>();
        }        

        private async void ImageButtonClick(object obj)
        {

            FileData fileData = await TakePhotoAsync();
            DisplayOrientation orientation = DeviceDisplay.MainDisplayInfo.Orientation;
            if (fileData != null && !string.IsNullOrEmpty(fileData.FileName))
            {
                ImageSource DisplayPhoto = ImageSource.FromStream(() =>
                new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                

                var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);

                if (Photoaction != "Gallery")
                {
                    obMarketInsightFileRequest.Add
                    (new MarketInsightFileRequest
                    {
                        FileInfo = new FileInfoResponse
                        {
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.MarketIntelImageFile,
                            Base64StringImage = fileData.string64baseData,
                            Rotation = (orientation == DisplayOrientation.Landscape) ? 0 : 90
                        }
                    });             
                }
                else
                {
                    obMarketInsightFileRequest.Add
                    (new MarketInsightFileRequest
                    {
                        FileInfo = new FileInfoResponse
                        {
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.MarketIntelImageFile,
                            Base64StringImage = fileData.string64baseData,
                            Rotation = 0
                        }
                    });
                }
                   
                
                //}
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();
                });

            }
        }

        public ICommand DeleteVideoCommand
        {
            get
            {
                return new Command<MarketInsightFileRequest>(async (item) =>
                {
                    obMarketInsightFileRequestVideo.Remove(item);

                });

            }
        }

        public ICommand DeletePhotoCommand
        {
            get
            {
                return new Command<MarketInsightFileRequest>(async (item) =>
                {
                    obMarketInsightFileRequest.Remove(item);

                });

            }
        }

        public Command PickImageCommand
        {
            get
            {
                return new Command(async () =>
                {
                //string action = await Application.Current.MainPage.DisplayActionSheet("Photo Upload", "Cancel", null, "Take Photo", "Gallery");
                    var pickresult = await FilePicker.PickMultipleAsync(new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images,
                        PickerTitle = "Pick Images"
                    });

                    if (pickresult != null)
                    {
                        var ImageList = new List<ImageSource>();
                        foreach (var image in pickresult)
                        {
                            var stream = await image.OpenReadAsync();
                            ImageList.Add(ImageSource.FromStream(()=> stream));
                        }
                    }
                });
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsBusy = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;
                        await SaveMarketInsight();
                        IsBusy = false;
                    });
                });
            }
        }

        async Task SaveMarketInsight()
        {

            try
            {
                IsBusy = true;
                
                if (NetworkAvailable)
                {
                    //if (obMarketInsightFileRequest == null || obMarketInsightFileRequest.Count == 0)
                    //{
                    //    await ErrorDisplayAlert("Please upload photos.");
                    //    return;
                    //}
                    //if (obMarketInsightFileRequestVideo == null || obMarketInsightFileRequestVideo.Count == 0)
                    //{
                    //    await ErrorDisplayAlert("Please upload videos.");
                    //    return;
                    //}

                    if (MarketIntelComment == null || MarketIntelComment == string.Empty)
                    {
                        await ErrorDisplayAlert("Please add comment.");
                        return;
                    }
                    objMarketInsightRequest = new MarketInsightRequest();

                    ObservableCollection<MarketInsightFileRequest> marketInsights
                        = new ObservableCollection<MarketInsightFileRequest>
                        (obMarketInsightFileRequest.Union(obMarketInsightFileRequestVideo));

                    lstMarketInsightFileRequest = new List<MarketInsightFileRequest>(marketInsights);
                    MarketIntelDataManagementSL marketIntelDataManagementSL = new MarketIntelDataManagementSL();
                    objMarketInsightRequest.Comment = MarketIntelComment;
                    objMarketInsightRequest.MarketInsightFiles = lstMarketInsightFileRequest;
                    objMarketInsightRequest.CreationDate = DateTime.Now;
                    objMarketInsightRequest.CreatedBy = CommonAttribute.CustomeProfile.PersonId;
                    objMarketInsightRequest.LocationId = CommonAttribute.CustomeProfile.Locations?.FirstOrDefault().LocationId;
                  //  await Task.Delay(20);
                    var receiveContext = await marketIntelDataManagementSL.SaveMarketInsight(objMarketInsightRequest);
                    if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                    {
                        await Navigation.PushAsync(new FeedBackSuccessPage("MarketInsights"));
                    }
                    else if (receiveContext != null)
                    {
                        await ErrorDisplayAlert(receiveContext.ErrorMessage);
                    }
                    else
                    {
                        await ErrorDisplayAlert(Resx.AppResources.lblErrorTitle);
                    }
                }
                else
                {
                    showToasterNoNetwork();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command RecordVideoCommand
        {
            get
            {
                return new Command(async () =>
                {
                  
                    FileData fileData = await TakeVideoAsync();
                    if (fileData != null && !string.IsNullOrEmpty(fileData.FileName))
                    {
                        ImageSource DisplayVideo = ImageSource.FromStream(() =>
                        new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                        var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);

                        obMarketInsightFileRequestVideo.Add
                        (new MarketInsightFileRequest
                        {
                            FileInfo = new FileInfoResponse
                            {
                                TempPath = newFile,
                                FileName = fileData.FileName,
                                FileTypeId = (int)FileTypeEnum.MarketIntelVideoFile,
                                Base64StringImage = fileData.string64baseData
                            }
                        });                        
                    }

                });
            }
        }

        public string Photoaction { get; set; }

        #region Photo
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
                    photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
                    {
                        
                    });
                    //var pickresult = await FilePicker.PickMultipleAsync(new PickOptions
                    //{
                    //    FileTypes = FilePickerFileType.Images,                        
                    //    PickerTitle = "Pick Images"
                    //});

                    //if (pickresult != null)
                    //{
                    //    var ImageList = new List<ImageSource>();
                    //    foreach (var image in pickresult)
                    //    {
                    //        var stream = await image.OpenReadAsync();
                    //        ImageList.Add(ImageSource.FromStream(() => stream));
                    //    }
                    //    if (ImageList != null)
                    //    {
                    //        Application.Current.Properties["Photoaction"] = Photoaction;
                    //        await Application.Current.SavePropertiesAsync();
                    //        return fileData = await LoadPhotoAsync(ImageList);
                    //    }
                        
                   // }
                }
                else if (action != null && action == "Take Photo")
                {
                    Photoaction = action;
                    if (!MediaPicker.IsCaptureSupported)
                    {
                        await ErrorDisplayAlert("Camera not supported");
                        return fileData;
                    }
                    
                    photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                    {

                    });
                    
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
                    return null;
                }
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
                        var resizedBytes = resizer.ResizeImage(originalImageByteArray, 1024, 1024);

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
        #endregion

        #region Video
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
                    return fileData;
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
        #endregion

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

        private MarketInsightRequest _objMarketInsightRequest;
        public MarketInsightRequest objMarketInsightRequest

        {
            get { return _objMarketInsightRequest; }
            set
            {
                _objMarketInsightRequest = value;
                OnPropertyChanged(nameof(objMarketInsightRequest));
            }
        }

        private List<MarketInsightFileRequest> _lstMarketInsightFileRequest;
        public List<MarketInsightFileRequest> lstMarketInsightFileRequest

        {
            get { return _lstMarketInsightFileRequest; }
            set
            {
                _lstMarketInsightFileRequest = value;
                OnPropertyChanged(nameof(lstMarketInsightFileRequest));
            }
        }

        private ObservableCollection<MarketInsightFileRequest> _obMarketInsightFileRequest;
        public ObservableCollection<MarketInsightFileRequest> obMarketInsightFileRequest

        {
            get { return _obMarketInsightFileRequest; }
            set
            {
                _obMarketInsightFileRequest = value;
                OnPropertyChanged(nameof(obMarketInsightFileRequest));
            }
        }

        private ObservableCollection<MarketInsightFileRequest> _obMarketInsightFileRequestVideo;
        public ObservableCollection<MarketInsightFileRequest> obMarketInsightFileRequestVideo

        {
            get { return _obMarketInsightFileRequestVideo; }
            set
            {
                _obMarketInsightFileRequestVideo = value;
                OnPropertyChanged(nameof(obMarketInsightFileRequestVideo));
            }
        }

        private string _MarketIntelComment;
        public string MarketIntelComment

        {
            get { return _MarketIntelComment; }
            set
            {
                _MarketIntelComment = value;
                OnPropertyChanged(nameof(MarketIntelComment));
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ServiceLayer;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Models;
using Retail.Views.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;

namespace Retail.ViewModels.Account
{
    public class MyProfilePageViewModel : BaseViewModel
    {
        public string PersonProfileStatus { get; set; }

        public MyProfilePageViewModel(INavigation navigation) : base(navigation)
        {
            languages = Extensions.Getlanguages();           
            Email = CommonAttribute.CustomeProfile?.Email;
            MobileNumber = CommonAttribute.CustomeProfile?.MobileNumber;
            Name = CommonAttribute.CustomeProfile?.FirstName + " " + CommonAttribute.CustomeProfile?.LastName;
            Country = CommonAttribute.CustomeProfile?.CountryName;
            Status = CommonAttribute.CustomeProfile?.PersonProfileStatus;
            IsProfileStatusVisible = true;
            if (Status != null && Status != string.Empty && Status != "")
            {
                IsProfileStatusVisible = true;
                PersonProfileStatus = Status.ToLower().Trim();

                if (PersonProfileStatus == ProfileStatusEnum.Gold.ToString().ToLower().Trim())
                    StatusImage = "gold";
                else if (PersonProfileStatus == ProfileStatusEnum.Silver.ToString().ToLower().Trim())
                    StatusImage = "silver";
                else if (PersonProfileStatus == ProfileStatusEnum.Platinum.ToString().ToLower().Trim())
                    StatusImage = "platinum";
                else if (PersonProfileStatus == ProfileStatusEnum.Bronze.ToString().ToLower().Trim())
                    StatusImage = "bronz";
                else
                    StatusImage = "";
            }
            else
            {
                IsProfileStatusVisible = false;
            }

            if (string.IsNullOrEmpty(MobileNumber))
                IsVisibleMobNo = false;
            else
                IsVisibleMobNo = true;
        }
        public List<LanguageModel> languages { get; set; }
        public LanguageModel SelectedLanguage { get; set; }
        public Command UserPicCommand
        {
            get
            {
                return new Command(async () =>
                {
                    FileData fileData = await TakePhotoAsync();
                    UserPic = ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));
                    var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);
                    Application.Current.Properties["userpic"] = UserPic;


                });
            }
        }
        
        public async Task SaveUserPic(FileData fileData)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    UserManagementSL userManagementSL = new UserManagementSL();
                    FilesToUpload filesToUpload = new FilesToUpload();
                    filesToUpload.fileInfoId = 0;
                    filesToUpload.fileName = fileData.FileName;
                    filesToUpload.mimeType = fileData.FileType;
                    filesToUpload.path = fileData.FileName;
                    filesToUpload.fileTypeId = 5; //Convert.ToInt16(FileTypeEnum.PersonProfileImage);
                    filesToUpload.base64StringImage = fileData.string64baseData;

                    var resp = await userManagementSL.SavePersonProfilePicture(CommonAttribute.CustomeProfile.PersonId, filesToUpload);
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

        private ImageSource _StatusImage;
        public ImageSource StatusImage
        {
            get { return _StatusImage; }
            set
            {
                _StatusImage = value;
                OnPropertyChanged(nameof(StatusImage));
            }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }


        private string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                OnPropertyChanged("Name");
            }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {

                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {

                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                 
                _MobileNumber = value;
                OnPropertyChanged("MobileNumber");
            }
        }

        public Boolean _IsProfileStatusVisible;
        public Boolean IsProfileStatusVisible
        {
            get { return _IsProfileStatusVisible; }
            set
            {
                _IsProfileStatusVisible = value;
                OnPropertyChanged(nameof(IsProfileStatusVisible));
            }
        }

        public Boolean _IsVisibleMobNo;
        public Boolean IsVisibleMobNo
        {
            get { return _IsVisibleMobNo; }
            set
            {
                _IsVisibleMobNo = value;
                OnPropertyChanged("IsVisibleMobNo");
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

                if (action!=null && action == "Gallery")
                {
                    Photoaction = action;
                     photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() {  });// CapturePhotoAsync();
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


                        string string64base = Extensions.ConvertToBase64(stream);
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
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Retail.Hepler;
using Retail.DependencyServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using System.Collections.Generic;

namespace Retail.ViewModels.DemoControls
{
    public class TakePhotoAndDisplayViewModel : INotifyPropertyChanged
    {
        [Obsolete]
        public TakePhotoAndDisplayViewModel(IMultiMediaPickerService multiMediaPickerService)
        {
            //_multiMediaPickerService = media;
            //SelectImagesCommand = new Command(async (obj) =>
            //{
            //    Media = new ObservableCollection<MediaFile>();
            //    await _multiMediaPickerService.PickPhotosAsync();                
            //});

            _multiMediaPickerService = multiMediaPickerService;
            SelectImagesCommand = new Command(async (obj) =>
            {
                //var hasPermission = await CheckPermissionsAsync();
                //if (hasPermission)
                //{
                //    Media = new ObservableCollection<MediaFile>();
                //    await _multiMediaPickerService.PickPhotosAsync();
                //}


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
                        ImageList.Add(ImageSource.FromStream(() => stream));
                    }
                }
            });
            try
            {
                _multiMediaPickerService.OnMediaPicked += (s, a) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Media.Add(a);

                    });

                };
            }
            catch (Exception ex)
            { }

        }
        public string Photoaction { get; set; }
        public ObservableCollection<MediaFile> Media { get; set; }
        public ICommand SelectImagesCommand { get; set; }
        IMultiMediaPickerService _multiMediaPickerService;

        public event PropertyChangedEventHandler PropertyChanged;

        [Obsolete]
        async Task<bool> CheckPermissionsAsync()
        {
            var retVal = false;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Need Storage permission to access to your photos.", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    retVal = true;

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("Alert", "Error. Can not continue, try again.", "Ok");
            }

            return retVal;

        }
        
    }
}


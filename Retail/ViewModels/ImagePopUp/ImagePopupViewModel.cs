using System;
using System.Windows.Input;
using Retail.DependencyServices;
using Retail.Hepler;
using Xamarin.Forms;

namespace Retail.ViewModels.ImagePopUp
{
    public class ImagePopupViewModel : BaseViewModel
    {
        string downloadimage { get; set; }
        public ICommand DownloadImageCommad { get; set; }

        public ImagePopupViewModel(INavigation navigation , string _ImageURL) : base(navigation)
        {
            downloadimage = _ImageURL;
            DownloadImageCommad = new Command(DownloadImageUrl);
        }

        private async void DownloadImageUrl(object obj)
        {
            try
            {
                //IsBusy = true;

                string imgUrl = downloadimage;
                byte[] bytes = await Extensions.DownloadImageAsync(imgUrl);
                if (bytes != null)
                {
                    var hud = DependencyService.Get<IMediaService>();
                    if (hud != null)
                    {
                        string filename = System.IO.Path.GetFileName(imgUrl);

                        hud.SaveImageFromByte(bytes, filename);
                        await DisplayAlert("Success", "Product Catalogue downloaded.");
                    }

                }
                else
                {
                    await ErrorDisplayAlert("Your product catalogue not yet ready.");
                }

                //IsBusy = false;

            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        //public Command DownloadImageCommad
        //{
        //    get
        //    {
        //        return new Command<string>(async (item) =>
        //        {
        //        });
        //    }
        //}
    }
}


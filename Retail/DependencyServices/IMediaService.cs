using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Models;

namespace Retail.DependencyServices
{
    public interface IMediaService
    {
        //void OpenGallery();
        //void ClearFileDirectory();
        void SaveImageFromByte(byte[] imageByte, string filename);
        string GetBaseUrl();

    }

    public interface IMultiMediaPickerService
    {
        event EventHandler<MediaFile> OnMediaPicked;
        event EventHandler<IList<MediaFile>> OnMediaPickedCompleted;
        Task<IList<MediaFile>> PickPhotosAsync();
        Task<IList<MediaFile>> PickVideosAsync();
        void Clean();
    }
}

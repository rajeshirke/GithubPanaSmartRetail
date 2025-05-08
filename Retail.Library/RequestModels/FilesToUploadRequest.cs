using System;
using Newtonsoft.Json;
using Retail.Infrastructure.ResponseModels;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Retail.Infrastructure.RequestModels
{
    public class FilesToUploadRequest
    {
        public int FileInfoId { get; set; }
        public string FileName { get; set; }
        public int FileTypeId { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
        public string base64StringImage { get; set; }

        [JsonIgnore]
        public ImageSource DisplayPhoto
        {
            get
            {
                return ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(base64StringImage))
                );
            }
        }

        [JsonIgnore]
        public string TempPath { get; set; }

        [JsonIgnore]
        public Task<FileStream> DisplayVideo
        {
            get
            {
                return FileFormatChanger.CopyVideoIfNotExists(TempPath, FileName);
            }
        }

        [JsonIgnore]
        public string FileFullPath { get; set; }

        [JsonIgnore]
        public int Rotation { get; set; }
    }
}


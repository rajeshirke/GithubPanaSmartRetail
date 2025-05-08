using Newtonsoft.Json;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Retail.Infrastructure.ResponseModels
{
    public class FileInfoResponse
    {
        public int FileInfoId { get; set; }
        public string FileName { get; set; }
        public int? FileTypeId { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }

        public string Base64StringImage { get; set; }

        [JsonIgnore]
        public ImageSource DisplayPhoto
        {
            get
            {
                return ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(Base64StringImage))
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

    public static class FileFormatChanger
    {
        public static async Task<FileStream> CopyVideoIfNotExists(string TempPath, string FileName)
        {
            FileStream outputStream1 = null;
            if (!File.Exists(TempPath))
            {
                using (Stream inputStream = await FileSystem.OpenAppPackageFileAsync(FileName))
                {
                    outputStream1 = File.Create(TempPath);
                }
            }
            return outputStream1;
        }
    }
}

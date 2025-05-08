using System;
namespace Retail.Infrastructure.Models
{
    public class FilesToUpload
    {
        public int fileInfoId { get; set; }
        public string fileName { get; set; }
        public int fileTypeId { get; set; }
        public string mimeType { get; set; }
        public string path { get; set; }
        public string base64StringImage { get; set; }
    }
}

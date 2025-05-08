using System;
using System.IO;

namespace Retail.Models
{
    public class FileData
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string string64baseData { get; set; }
        public Stream imgStream { get; set; }
    }

    public enum MediaFileType
    {
        Image,
        Video
    }
    public class MediaFile
    {
        public string PreviewPath { get; set; }
        public string Path { get; set; }
        public MediaFileType Type { get; set; }
    }
}

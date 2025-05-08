using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class FileInfo
    {
        public FileInfo()
        {
            AnswerUploadedFiles = new HashSet<AnswerUploadedFile>();
            LocationFiles = new HashSet<LocationFile>();
            MessageTemplateFileInfoes = new HashSet<MessageTemplateFileInfo>();
            Persons = new HashSet<Person>();
            ProductCatalogues = new HashSet<ProductCatalogue>();
            ProductFiles = new HashSet<ProductFile>();
        }

        public long FileInfoId { get; set; }
        public string FileName { get; set; }
        public int? FileTypeId { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }

        public virtual FileType FileType { get; set; }
        public virtual ICollection<AnswerUploadedFile> AnswerUploadedFiles { get; set; }
        public virtual ICollection<LocationFile> LocationFiles { get; set; }
        public virtual ICollection<MessageTemplateFileInfo> MessageTemplateFileInfoes { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<ProductCatalogue> ProductCatalogues { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }
}

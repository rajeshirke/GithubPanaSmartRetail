using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class FileType
    {
        public FileType()
        {
            FileInfoes = new HashSet<FileInfo>();
        }

        public int FileTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FileInfo> FileInfoes { get; set; }
    }
}

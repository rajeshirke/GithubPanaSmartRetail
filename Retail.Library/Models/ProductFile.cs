using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ProductFile
    {
        public long ProductModelFileInfoId { get; set; }
        public long ProductModelId { get; set; }
        public long FileInfoId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}

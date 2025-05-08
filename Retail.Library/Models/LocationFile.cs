using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class LocationFile
    {
        public int LocationFileId { get; set; }
        public int LocationId { get; set; }
        public long FileInfoId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
        public virtual Location Location { get; set; }
    }
}

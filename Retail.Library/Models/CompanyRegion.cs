using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class CompanyRegion
    {
        public int CompanyRegionId { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Region Region { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Region
    {
        public Region()
        {
            CompanyRegions = new HashSet<CompanyRegion>();
            Countries = new HashSet<Country>();
        }

        public int RegionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyRegion> CompanyRegions { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}

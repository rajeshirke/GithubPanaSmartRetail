using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class LocationAccount
    {
        public LocationAccount()
        {
            Locations = new HashSet<Location>();
        }

        public int LocationAccountId { get; set; }
        public string LocationAccountName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}

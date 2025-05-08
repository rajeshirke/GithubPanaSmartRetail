using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonCountry
    {
        public long PersonCountryId { get; set; }
        public long PersonId { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Person Person { get; set; }
    }
}

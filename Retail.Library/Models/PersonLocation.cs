using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonLocation
    {
        public long PersonLocationId { get; set; }
        public long PersonId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Person Person { get; set; }
    }
}

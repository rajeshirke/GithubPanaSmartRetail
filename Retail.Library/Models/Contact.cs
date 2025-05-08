using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public int LocationId { get; set; }
        public int? ContactTypeId { get; set; }
        public string ContactValue { get; set; }

        public virtual Location Location { get; set; }
    }
}

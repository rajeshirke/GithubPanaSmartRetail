using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonServiceCenter
    {
        public int PersonServiceCenterId { get; set; }
        public long PersonId { get; set; }
        public int ServiceCenterId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ServiceCenter ServiceCenter { get; set; }
    }
}

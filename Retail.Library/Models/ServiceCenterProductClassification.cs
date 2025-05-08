using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ServiceCenterProductClassification
    {
        public int Id { get; set; }
        public int ServiceCenterId { get; set; }
        public int? ProductClassificationId { get; set; }

        public virtual ServiceCenter ServiceCenter { get; set; }
    }
}

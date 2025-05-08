using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Attribute
    {
        public long AttributeId { get; set; }
        public string Name { get; set; }
        public int EntityId { get; set; }
        public string AttributeValue { get; set; }

        public virtual Entity Entity { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntelType
    {
        public MarketIntelType()
        {
            MarketIntels = new HashSet<MarketIntel>();
        }

        public int MarketIntelTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MarketIntel> MarketIntels { get; set; }
    }
}

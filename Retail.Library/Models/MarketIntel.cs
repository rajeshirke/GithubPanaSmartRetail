using System;
using System.Collections.Generic;
using Retail.Infrastructure.Enums;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntel
    {
        public MarketIntel()
        {
            MarketIntelCollections = new HashSet<MarketIntelCollection>();
            MarketIntelQuestionMasters = new HashSet<MarketIntelQuestionMaster>();
        }

        public int MarketIntelId { get; set; }
        public string MarketIntelName { get; set; }
        public int? RegionId { get; set; }
        public int CountryId { get; set; }
        public int MarketIntelTypeId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Timer { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        

        public virtual Country Country { get; set; }
        public virtual MarketIntelType MarketIntelType { get; set; }
        public virtual ICollection<MarketIntelCollection> MarketIntelCollections { get; set; }
        public virtual ICollection<MarketIntelQuestionMaster> MarketIntelQuestionMasters { get; set; }

        public bool IsExpiryDateVisible
        {
            get
            {
                if (MarketIntelTypeId == (int)MarketIntelTypeEnum.ProductTest)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsTimerVisible
        {
            get
            {
                if (MarketIntelTypeId == (int)MarketIntelTypeEnum.ProductTest)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}

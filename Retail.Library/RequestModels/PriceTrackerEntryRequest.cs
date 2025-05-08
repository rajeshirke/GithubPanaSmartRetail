using System;
using System.Collections.Generic;
using Retail.Infrastructure.ResponseModels;

namespace Retail.Infrastructure.RequestModels
{
    public class PriceTrackerEntryRequest
    {
        public DateTime Entrydate { get; set; }
        public int LocationId { get; set; }
        public long ProductModelId { get; set; }
        public string Panasonic_ModelNumber { get; set; }
        public String Comment { get; set; }
        public long PersonId { get; set; }
        public virtual ICollection<PriceTrackerResponse> PriceTrackerResponses { get; set; }
    }

    //public class PriceTrackerCompetitorModelEntryRequest
    //{
    //    public int CompetitorProductModelId { get; set; }
    //    public string Comp_ModelNumber { get; set; }
    //    public decimal RRP { get; set; }
    //    public decimal NetRRP { get; set; }
    //    public decimal Promo { get; set; }
    //}
}


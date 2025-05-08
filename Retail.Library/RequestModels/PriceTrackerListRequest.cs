using System;
namespace Retail.Infrastructure.RequestModels
{
    public class PriceTrackerListRequest
    {
        public long ModelId { get; set; }
        public long PersonId { get; set; }
        public int StoreId { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}


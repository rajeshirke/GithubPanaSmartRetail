using System;
namespace Retail.Infrastructure.RequestModels
{
    public class DisplayTrackerRequest
    {
        public long PersonId { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int LocationId { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}


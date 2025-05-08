using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntelCollection
    {
        public MarketIntelCollection()
        {
            MarketIntelQuestionAnswers = new HashSet<MarketIntelQuestionAnswer>();
        }

        public long MarketIntelCollectionId { get; set; }
        public int MarketIntelId { get; set; }
        public long AttendedByPersonId { get; set; }
        public DateTime AttendedDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int StatusId { get; set; }

        public virtual Person AttendedByPerson { get; set; }
        public virtual MarketIntel MarketIntel { get; set; }
        public virtual ICollection<MarketIntelQuestionAnswer> MarketIntelQuestionAnswers { get; set; }
    }
}

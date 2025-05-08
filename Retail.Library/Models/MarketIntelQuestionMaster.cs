using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntelQuestionMaster
    {
        public MarketIntelQuestionMaster()
        {
            MarketIntelQuestionAnswers = new HashSet<MarketIntelQuestionAnswer>();
        }

        public long MarketIntelQuestionMasterId { get; set; }
        public long QuestionMasterId { get; set; }
        public int MarketIntelId { get; set; }

        public virtual MarketIntel MarketIntel { get; set; }
        public virtual QuestionMaster QuestionMaster { get; set; }
        public virtual ICollection<MarketIntelQuestionAnswer> MarketIntelQuestionAnswers { get; set; }
    }
}

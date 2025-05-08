using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MarketIntelAnswerSelectedOption
    {
        public int MarketIntelAnswerSelectedOptionId { get; set; }
        public long MarketIntelQuestionAnswerId { get; set; }
        public long OptionId { get; set; }

        public virtual MarketIntelQuestionAnswer MarketIntelQuestionAnswer { get; set; }
        public virtual AnswerOption Option { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AnswerSelectedOption
    {
        public long AnswerSelectedOptionId { get; set; }
        public long? OptionId { get; set; }
        public long TaskQuestionAnswerId { get; set; }

        public virtual AnswerOption Option { get; set; }
        public virtual TaskQuestionAnswer TaskQuestionAnswer { get; set; }
    }
}

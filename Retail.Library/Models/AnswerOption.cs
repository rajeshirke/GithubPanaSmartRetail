using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AnswerOption
    {
        public AnswerOption()
        {
            AnswerSelectedOptions = new HashSet<AnswerSelectedOption>();
        }

        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public long QuestionMasterId { get; set; }
        public bool OptionSelected { get; set; }

        public virtual QuestionMaster QuestionMaster { get; set; }
        public virtual ICollection<AnswerSelectedOption> AnswerSelectedOptions { get; set; }
    }
}

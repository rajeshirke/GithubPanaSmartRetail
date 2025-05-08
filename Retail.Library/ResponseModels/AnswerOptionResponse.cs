using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class AnswerOptionResponse
    {
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public bool OptionSelected { get; set; }
        public long QuestionMasterId { get; set; }

        //public virtual ICollection<AnswerSelectedOptionResponse> AnswerSelectedOptions { get; set; }
    }
}

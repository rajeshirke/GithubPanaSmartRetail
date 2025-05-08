using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class AnswerSelectedOptionResponse
    {
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public long QuestionMasterId { get; set; }
    }
}

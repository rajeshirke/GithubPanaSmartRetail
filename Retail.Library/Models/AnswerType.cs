using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AnswerType
    {
        public AnswerType()
        {
            QuestionMasters = new HashSet<QuestionMaster>();
        }

        public int AnswerTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuestionMaster> QuestionMasters { get; set; }
    }
}

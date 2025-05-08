using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class AnswerStatu
    {
        public AnswerStatu()
        {
            TaskQuestionAnswers = new HashSet<TaskQuestionAnswer>();
        }

        public int AnswerStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskQuestionAnswer> TaskQuestionAnswers { get; set; }
    }
}

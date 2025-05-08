using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class QuestionMaster
    {
        public QuestionMaster()
        {
            AnswerOptions = new HashSet<AnswerOption>();
            TaskMasterQuestions = new HashSet<TaskMasterQuestion>();
            TaskQuestionAnswers = new HashSet<TaskQuestionAnswer>();
        }

        public long QuestionMasterId { get; set; }
        public string QuestionTitle { get; set; }
        public string Description { get; set; }
        public int AnswerTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMandatory { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual AnswerType AnswerType { get; set; }
        public virtual ICollection<AnswerOption> AnswerOptions { get; set; }
        public virtual ICollection<TaskMasterQuestion> TaskMasterQuestions { get; set; }
        public virtual ICollection<TaskQuestionAnswer> TaskQuestionAnswers { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskQuestionAnswer
    {
        public TaskQuestionAnswer()
        {
            AnswerSelectedOptions = new HashSet<AnswerSelectedOption>();
            AnswerUploadedFiles = new HashSet<AnswerUploadedFile>();
        }

        public long TaskQuestionAnswerId { get; set; }
        public string AnswerText { get; set; }
        public string AanswerSign { get; set; }
        public string AnswerBarcode { get; set; }
        public bool? AnswerYesNo { get; set; }
        public int? AnswerNumber { get; set; }
        public DateTime? AnswerDate { get; set; }
        public decimal? AnswerPrice { get; set; }
        public string AnswerQrcode { get; set; }
        public string AnswerCurrency { get; set; }
        public long? AnswerSelectedOptionId { get; set; }
        public string AnswerSelectedOptionText { get; set; }
        public int? StatusId { get; set; }
        public long? AnswerEntryByPersonId { get; set; }
        public DateTime AnswerEntryDate { get; set; }
        public long VisitLocationTaskId { get; set; }
        public long QuestionMasterId { get; set; }
        public long TaskMasterId { get; set; }

        public virtual QuestionMaster QuestionMaster { get; set; }
        public virtual AnswerStatu Status { get; set; }
        public virtual VisitLocationTask VisitLocationTask { get; set; }
        public virtual ICollection<AnswerSelectedOption> AnswerSelectedOptions { get; set; }
        public virtual ICollection<AnswerUploadedFile> AnswerUploadedFiles { get; set; }
    }
}

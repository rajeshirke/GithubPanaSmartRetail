using Newtonsoft.Json;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class TaskQuestionAnswerToSubmitResponse
    {

        public long VisitLocationTaskId { get; set; }

        public long TaskMasterId { get; set; }

        //-----------QUESTION MASTER

        public long QuestionMasterId { get; set; }
        public string QuestionTitle { get; set; }
        public string Description { get; set; }
        public int AnswerTypeId { get; set; }
        public int SrNo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMandatory { get; set; }
        public int SerialNo { get; set; }
        public ICollection<AnswerOptionResponse> AnswerOptions { get; set; }

        //-----------TASK QUESTION ANSWER
        public long? TaskQuestionAnswerId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerSign { get; set; }
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
        public DateTime? AnswerEntryDate { get; set; }

        [JsonIgnore]
        public bool? AnswerNo { get; set; }
        [JsonIgnore]
        public bool IsTaskEditable { get; set; } = true;

        //  public virtual QuestionMasterResponse QuestionMaster { get; set; }

        public virtual ICollection<AnswerSelectedOptionResponse> AnswerSelectedOptions { get; set; }
        public ICollection<AnswerUploadedFileResponse> AnswerUploadedFiles { get; set; }

    }
}

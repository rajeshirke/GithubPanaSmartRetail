using Newtonsoft.Json;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class TaskQuestionAnswerResponse
    {
        public QuestionMasterResponse QuestionMaster { get; set; }
        public long TaskQuestionAnswerId { get; set; }
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
        public DateTime AnswerEntryDate { get; set; }
        public long VisitLocationTaskId { get; set; }
        public long QuestionMasterId { get; set; }
        public long TaskMasterId { get; set; }

        [JsonIgnore]
        public string AnswerStatusName { get { return Enum.GetName(typeof(AnswerStatusEnum), StatusId); } }
        //public virtual AnswerStatu Status { get; set; }
        //public virtual TaskMaster TaskMaster { get; set; } 

      
        //public virtual VisitLocationTask VisitLocationTask { get; set; }
        public ICollection<AnswerSelectedOptionResponse> AnswerSelectedOptions { get; set; }
        public  ICollection<AnswerUploadedFileResponse> AnswerUploadedFiles { get; set; }
    }
}

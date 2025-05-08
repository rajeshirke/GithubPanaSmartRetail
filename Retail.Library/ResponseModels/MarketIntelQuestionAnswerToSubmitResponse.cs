using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class MarketIntelQuestionAnswerToSubmitResponse
    {
        //-----------QUESTION MASTER

        public int SrNo { get; set; }
        public long QuestionMasterId { get; set; }
        public string QuestionTitle { get; set; }
        public string Description { get; set; }
        public int AnswerTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMandatory { get; set; }
        public ICollection<AnswerOptionResponse> AnswerOptions { get; set; }



        //-----------MARKET INTEL   QUESTION ANSWER
        public long MarketIntelQuestionAnswerId { get; set; }
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
        //public long QuestionMasterId { get; set; }
        public long MarketIntelQuestionMasterId { get; set; }
        public long MarketIntelCollectionId { get; set; }
        public int? MarketIntelId { get; set; }

        //public virtual MarketIntelQuestionMaster MarketIntelQuestionMaster { get; set; }
        //public virtual QuestionMaster QuestionMaster { get; set; }
        //public virtual AnswerStatu Status { get; set; }
        public virtual ICollection<AnswerSelectedOptionResponse> MarketIntelAnswerSelectedOptions { get; set; }
        public virtual ICollection<AnswerUploadedFileResponse> MarketIntelAnswerUploadedFiles { get; set; }

    }
}

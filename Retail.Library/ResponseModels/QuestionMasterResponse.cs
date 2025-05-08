using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class QuestionMasterResponse
    {
        //public QuestionMaster()
        //{
        //    AnswerOptions = new HashSet<AnswerOption>();
        //    TaskMasterQuestions = new HashSet<TaskMasterQuestion>();
        //}

        public long QuestionMasterId { get; set; }
        public string QuestionTitle { get; set; }
        public string Description { get; set; }
        public int AnswerTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMandatory { get; set; }
        //public long? CreatedBy { get; set; }
        //public DateTime? CreationDate { get; set; }

        //public virtual AnswerType AnswerType { get; set; }

        // public virtual string AnswerTypeName { get; set; }

        public string AnswerTypeName { get { return Enum.GetName(typeof(AnswerTypeEnum), AnswerTypeId); } }


        //Extensions.GetDisplayName((PartStockBucketEnum) cl.Key.PartStockBucketId)
        public virtual ICollection<AnswerOptionResponse> AnswerOptions { get; set; }
        //public virtual ICollection<TaskMasterQuestion> TaskMasterQuestions { get; set; }
    }
}

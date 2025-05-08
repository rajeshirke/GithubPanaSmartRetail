using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public  class TaskMasterQuestionResponse
    {
        public long TaskQuestionId { get; set; }
        public long TaskMasterId { get; set; }
        public long QuestionMasterId { get; set; } 

        public   QuestionMasterResponse QuestionMaster { get; set; }
        //public virtual TaskMaster TaskMaster { get; set; }
        //public virtual ICollection<AnswerUploadedFile> AnswerUploadedFiles { get; set; }
        //public virtual ICollection<TaskQuestionAnswer> TaskQuestionAnswers { get; set; }
    }
}

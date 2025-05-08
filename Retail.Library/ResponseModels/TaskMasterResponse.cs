using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class TaskMasterResponse
    {
        public long TaskMasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }
        public int SerialNo { get; set; }
        public virtual ICollection<TaskMasterQuestionResponse> TaskMasterQuestions { get; set; }
        //public virtual ICollection<TaskQuestionAnswer> TaskQuestionAnswers { get; set; }
        //public virtual ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class TaskResponse
    {
        public long TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }
        public int TaskStatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
         
        public virtual ICollection<TaskMasterQuestionResponse> TaskQuestions { get; set; }
        //public virtual ICollection<TaskTemplateTask> TaskTemplateTasks { get; set; }
        //public virtual ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class TaskMasterRequest
    {
        //public TaskMasterRequest()
        //{
        //    TaskMasterQuestions = new HashSet<TaskMasterQuestion>();
        //    VisitLocationTasks = new HashSet<VisitLocationTask>();
        //}

        public long TaskMasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DivisionId { get; set; }
        public bool IsActive { get; set; } = true;
         
        public ICollection<TaskMasterQuestion> TaskMasterQuestions { get; set; }
        public ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

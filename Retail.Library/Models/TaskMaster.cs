using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskMaster
    {
        public TaskMaster()
        {
            TaskMasterQuestions = new HashSet<TaskMasterQuestion>();
            VisitLocationTasks = new HashSet<VisitLocationTask>();
        }

        public long TaskMasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TaskMasterQuestion> TaskMasterQuestions { get; set; }
        public virtual ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

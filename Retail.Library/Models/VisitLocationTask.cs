using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class VisitLocationTask
    {
        public VisitLocationTask()
        {
            TaskQuestionAnswers = new HashSet<TaskQuestionAnswer>();
        }

        public long VisitLocationTaskId { get; set; }
        public long TaskMasterId { get; set; }
        public long VisitScheduleLocationId { get; set; }
        public int? TaskStatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
        public long? AssignedToPersonId { get; set; }

        public virtual TaskMaster TaskMaster { get; set; }
        public virtual TaskStatu TaskStatus { get; set; }
        public virtual VisitScheduleLocation VisitScheduleLocation { get; set; }
        public virtual ICollection<TaskQuestionAnswer> TaskQuestionAnswers { get; set; }
    }
}

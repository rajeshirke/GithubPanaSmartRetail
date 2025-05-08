using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskStatu
    {
        public TaskStatu()
        {
            VisitLocationTasks = new HashSet<VisitLocationTask>();
        }

        public int TaskStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

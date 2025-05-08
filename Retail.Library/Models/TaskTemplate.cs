using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskTemplate
    {
        public TaskTemplate()
        {
            TaskTemplateTasks = new HashSet<TaskTemplateTask>();
        }

        public long TaskTemplateId { get; set; }
        public string TaskTemplateName { get; set; }
        public string Description { get; set; }
        public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Division Division { get; set; }
        public virtual ICollection<TaskTemplateTask> TaskTemplateTasks { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskTemplateTask
    {
        public long TaskTemplateTaskId { get; set; }
        public long TaskTemplateId { get; set; }
        public long TaskId { get; set; }

        public virtual TaskTemplate TaskTemplate { get; set; }
    }
}

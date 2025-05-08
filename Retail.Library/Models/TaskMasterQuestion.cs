using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TaskMasterQuestion
    {
        public long TaskMasterQuestionId { get; set; }
        public long TaskMasterId { get; set; }
        public long QuestionMasterId { get; set; }

        public virtual QuestionMaster QuestionMaster { get; set; }
        public virtual TaskMaster TaskMaster { get; set; }
    }
}

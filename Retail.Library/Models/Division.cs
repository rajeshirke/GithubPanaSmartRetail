using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Division
    {
        public Division()
        {
            PersonDivisions = new HashSet<PersonDivision>();
            TaskTemplates = new HashSet<TaskTemplate>();
        }

        public int DivisionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PersonDivision> PersonDivisions { get; set; }
        public virtual ICollection<TaskTemplate> TaskTemplates { get; set; }
    }
}

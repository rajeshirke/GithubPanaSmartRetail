using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class VisitStatu
    {
        public VisitStatu()
        {
            VisitSchedules = new HashSet<VisitSchedule>();
        }

        public int VisitStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<VisitSchedule> VisitSchedules { get; set; }
    }
}

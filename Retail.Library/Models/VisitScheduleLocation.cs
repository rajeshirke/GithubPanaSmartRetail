using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class VisitScheduleLocation
    {
        public VisitScheduleLocation()
        {
            VisitLocationTasks = new HashSet<VisitLocationTask>();
        }

        public long VisitScheduleLocationId { get; set; }
        public long VisitScheduleId { get; set; }
        public int LocationId { get; set; }
        public int? RouteOptimizationOrder { get; set; }

        public virtual Location Location { get; set; }
        public virtual VisitSchedule VisitSchedule { get; set; }
        public virtual ICollection<VisitLocationTask> VisitLocationTasks { get; set; }
    }
}

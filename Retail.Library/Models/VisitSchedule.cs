using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class VisitSchedule
    {
        public VisitSchedule()
        {
            VisitScheduleLocations = new HashSet<VisitScheduleLocation>();
        }

        public long VisitScheduleId { get; set; }
        public string VisitCode { get; set; }
        public long PersonId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int VisitScheduleStatusId { get; set; }
        public DateTime VisitScheduleDate { get; set; }
        public DateTime? VisitStartDateTime { get; set; }
        public DateTime? VisitCompletionDateTime { get; set; }

        public virtual Person Person { get; set; }
        public virtual VisitStatu VisitScheduleStatus { get; set; }
        public virtual ICollection<VisitScheduleLocation> VisitScheduleLocations { get; set; }
    }
}

using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class VisitScheduleCreateUpdateRequest
    {

        public long VisitScheduleId { get; set; }
        public string VisitCode { get; set; }
        public long PersonId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int VisitScheduleStatusId { get; set; }
        public DateTime VisitScheduleDate { get; set; }
        public DateTime? VisitStartDateTime { get; set; }
        public DateTime? VisitCompletionDateTime { get; set; }
        public long? VisitScheduleEventId { get; set; }
        public virtual ICollection<VisitScheduleLocationRequest> VisitScheduleLocations { get; set; }

    }
}


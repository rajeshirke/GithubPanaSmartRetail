using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class VisitScheduleLocationRequest
    {
        public long VisitScheduleLocationId { get; set; }
        public long VisitScheduleId { get; set; }
        public int LocationId { get; set; }
        public string locationStoreName { get; set; }
        public int? RouteOptimizationOrder { get; set; }

        public virtual ICollection<VisitLocationTaskRequest> VisitLocationTasks { get; set; }

    }
}

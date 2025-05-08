using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class VisitLocationTaskRequest
    {
        public long VisitLocationTaskId { get; set; }
        public long TaskMasterId { get; set; }
        public string TaskMasterTitle { get; set; }
        
        public long VisitScheduleLocationId { get; set; }
        public int? TaskStatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
        public long? AssignedToPersonId { get; set; }
    }
}

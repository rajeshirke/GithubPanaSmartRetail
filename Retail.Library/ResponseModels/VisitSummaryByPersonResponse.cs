using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class VisitSummaryByPersonResponse
    {

        public long VisitId { get; set; }
        public string VisitCode { get; set; }
        public long PersonId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int VisitStatusId { get; set; }
        public DateTime PlannedVisitDate { get; set; } 
        public int VisitPendingCount { get; set; }
        public int VisitCompletedCount { get; set; }
        public int TaskPendingCount { get; set; }
        public int TaskCompletedCount { get; set; }

        public virtual PersonDetailsResponse Person { get; set; }


    }
}

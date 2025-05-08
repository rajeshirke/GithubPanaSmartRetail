using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class DailyVisitTargetTasksReportView
    {
        //public int LocationAccountId { get; set; }
        //public int PersonId { get; set; }
        //public string PersonName { get; set; }
        //public string VisitScheduleDate { get; set; }
        //public int TargetVistCount { get; set; }
        //public int Visitcount { get; set; }
        //public int TaskCount { get; set; }


        public int? LocationAccountId { get; set; }
        public int? LocationId { get; set; }
        public string LocationAccountName { get; set; }
        public string LocationName { get; set; }
        public long? PersonId { get; set; }
        public string PersonName { get; set; }
        public DateTime? VisitScheduleDate { get; set; }
        public int? TargetVistCount { get; set; }
        public int? VisitCount { get; set; }
        public int? TaskCount { get; set; }

    }
}

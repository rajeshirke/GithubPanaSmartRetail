using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class VisitScheduleResponse
    {

        public long VisitScheduleId { get; set; }
        public string VisitCode { get; set; }
        public long PersonId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public DateTime VisitScheduleDate { get; set; }

        public int VisitScheduleStatusId { get; set; }

        public string VisitScheduleStatusName { get { return Enum.GetName(typeof(VisitStatusEnum), VisitScheduleStatusId); } }

        //public virtual string PersonName { get; set; }
        //public virtual string PersonRoleName { get; set; }

        //public virtual Person Person { get; set; }
        //public virtual VisitStatu VisitScheduleStatus { get; set; }
        public virtual ICollection<VisitScheduleLocationResponse> VisitScheduleLocations { get; set; }
    }
}

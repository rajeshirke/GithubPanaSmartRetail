using Retail.Infrastructure.ExtraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class SupervisorTargetsOverviewResponse
    {
        public SalesTargetSummary  SalesTargetSummary { get; set; }
        public List<PersonLocationTarget> PersonLocationTargets { get; set; }
        public List<TargetSummaryByPersonLocationCategoryMonth> TargetSummaryByPersonLocationCategoryMonths { get; set; }

        
    }

    public class PersonLocationTarget
    {
        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public int? LocationID { get; set; }
        public string LocationStoreName { get; set; }
        public SalesTargetSummary SalesTargetSummary { get; set; }

    }
}

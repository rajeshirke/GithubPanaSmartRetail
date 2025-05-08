using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ExtraModels
{
    public class TargetSummaryByPersonLocationCategoryMonth
    {
        public long PersonId { get; set; }
        public int? LocationID { get; set; }
        public string LocationStoreName { get; set; }
        public int? ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public decimal? TotalTargets { get; set; }
        public decimal? AcheivedTargets { get; set; }
        public string PersonName { get; set; }
    }
}

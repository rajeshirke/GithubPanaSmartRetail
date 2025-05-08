using Retail.Infrastructure.ExtraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class PromoterTargetsOverviewResponse
    {
        public SalesTargetSummary MainTargetSummary { get; set; }
        public List<MasterCategoryTarget> MasterCategoryTargets { get; set; }
        public List<TargetSummaryByPersonLocationCategoryMonth> TargetSummaryByPersonLocationCategoryMonth { get; set; }
    }


    public class MasterCategoryTarget
    {

        public bool IsExpanded { get; set; }
        
        public SalesTargetSummaryByCategory MasterTargetSummary { get; set; }

        public List<SalesTargetSummaryByCategory> SubCategoryTargets { get; set; }
        public int SubCategoryHeight
        {
            get
            {
                if(SubCategoryTargets!=null)
                {
                    return SubCategoryTargets.Count * 30;
                }

                return 20;
            }
        }  //added by prj

    }
}

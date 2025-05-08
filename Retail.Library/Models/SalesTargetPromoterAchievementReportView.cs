using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesTargetPromoterAchievementReportView
    {
        //public long PersonId { get; set; }
        //public string PersonName { get; set; }
        //public int? TargetMonth { get; set; }
        //public int? TargetYear { get; set; }
        //public decimal? TotalTargets { get; set; }
        //public decimal? AcheivedTargets { get; set; }
        //public decimal? BalanceTargets { get; set; }
        //public int? PercentageAchieved { get; set; }
        //public int? LocationAccountId { get; set; }
        //public string LocationAccountName { get; set; }
        //public int? LocationId { get; set; }
        //public string LocationStoreName { get; set; }

        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public int? TargetMonth { get; set; }
        public int? TargetYear { get; set; }
        public decimal? TotalTargets { get; set; }
        public decimal? AcheivedTargets { get; set; }
        public decimal? BalanceTargets { get; set; }
        public decimal? PercentageAchieved { get; set; }
        public int? LocationAccountId { get; set; }
        public string LocationAccountName { get; set; }
        public int? LocationId { get; set; }
        public int? CountryId { get; set; }
        public string LocationStoreName { get; set; }

        public string TargetMonthNm { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SalesTargetCountryCityAccountReportView
    {
        public string Country { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int LocationAccountId { get; set; }
        public string LocationAccountName { get; set; }
        public int? TargetMonth { get; set; }
        public int? TargetYear { get; set; }
        public decimal? TotalTargets { get; set; }
        public decimal? AcheivedTargets { get; set; }
        public decimal? BalanceTargets { get; set; }
        public decimal? PercentageAchieved { get; set; }
    }
}

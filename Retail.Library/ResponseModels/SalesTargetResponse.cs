using Newtonsoft.Json;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Retail.Infrastructure.ResponseModels
{
    public class SalesTargetResponse
    {
        public long SalesTargetId { get; set; }
        public long PesronId { get; set; }
        public int TargetValue { get; set; }
        public int? AcheivedValue { get; set; }
        public int? LocationId { get; set; }
        public DateTime? TargetMonthYear { get; set; }
        public long TargetAssignedByPersonId { get; set; }
        public DateTime? TargetAssignedDate { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual PersonDetailsResponse Pesron { get; set; }
        public virtual ProductCategoryResponse ProductCategory { get; set; }
    }

    public class SalesTargetSummary
    {
        public int TotalTargets { get; set; }
        public int AchievedTargets { get; set; }
        public double AchievedTargetPercentage { get; set; }
        public int BalanceTargets { get; set; }
    }

    public class SalesTargetSummaryByCategory
    {
        public string SalesEntryUrl { get; set; } = "addnew.png"; //"salesentrydashboard.png";
        public string ArrowUrl { get; set; } = "nextnew.png";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int TotalTargets { get; set; }
        public int AchievedTargets { get; set; }
        public double AchievedTargetPercentage { get; set; }
        public int BalanceTargets { get; set; }

        
    }

    public class SalesTargetWithSummary
    { 
        public SalesTargetSummary SalesTargetSummary { get; set; }
        public List<SalesTarget> SalesTargets { get; set; }
    }

    public class SalesTargetWithSummaryResponse
    {
        public SalesTargetSummary SalesTargetSummary { get; set; }
        public List<SalesTargetResponse> SalesTargets { get; set; }
    }

    public class SalesTargetEntryResponse
    {
        public long SalesTargetEntryId { get; set; }
        public long ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime? TransactionDate {
            get {
                if (TargetInOutStatusId == (int)TargetInOutStatusEnum.Out)
                {
                    return CreatedDate;
                }
                else if (TargetInOutStatusId == (int)TargetInOutStatusEnum.In)
                {
                    return EntryDate;
                } else
                {
                    return EntryDate;
                }
            }
        }

        public int CurrencyId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? TargetInOutStatusId { get; set; }
        public long EntryByPersonId { get; set; }

        public string CurrencyCode { get; set; }
        public decimal TotalAmount
        {
            get
            {
                return Convert.ToDecimal(UnitPrice * Quantity);
            }
        }

        public Color ReturnEntry
        {
            get
            {
                if (TargetInOutStatusId != null)
                {
                    if(TargetInOutStatusId == (int)TargetInOutStatusEnum.Out)
                    {
                        return Color.Red;
                    }
                    else
                    {
                        return Color.Black;
                    }
                }
                return Color.Black;
            }
        }

        public string ReturnEntryStatus
        {
            get
            {
                if (TargetInOutStatusId != null)
                {
                    if (TargetInOutStatusId == (int)TargetInOutStatusEnum.Out)
                    {
                        return " Return";
                    }
                    else
                    {
                        return "";
                    }
                }
                return "";
            }
        }
    }


    
}

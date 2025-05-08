using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Retail.Infrastructure.RequestModels
{
    public class SalesEntryRequest
    {
        //public long SalesTargetEntryId { get; set; }
        //public long ProductModelId { get; set; }
        //public string ProductModelNumber { get; set; } 
        //public DateTime EntryDate { get; set; }
        ////public string Currency { get; set; }
        //public int CurrencyId { get; set; }
        //public int Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
        //public int? TargetInOutStatusId { get; set; }
        //public long EntryByPersonId { get; set; }


        public long SalesTargetEntryId { get; set; }
        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public long EntryByPersonId { get; set; }
        public DateTime EntryDate { get; set; }
        public int LocationId { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? CurrencyId { get; set; }
        public int TargetInOutStatusId { get; set; }
        public int? CountryId { get; set; } //new 24Aug21 prj

        [JsonIgnore]
        public string CurrencyCode { get; set; }
        [JsonIgnore]
        public bool SelectedReturn { get; set; }
        [JsonIgnore]
        public long SalesEntrySubmittedStatus { get; set; }
        //public virtual CurrencyMaster Currency { get; set; }
        //public virtual Person EntryByPerson { get; set; }
        //public virtual ProductModel ProductModel { get; set; }

    }
}

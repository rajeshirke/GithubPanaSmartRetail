using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Retail.Infrastructure.RequestModels
{
    public class InventoryStockEntryRequest
    {
        public long InventoryStockEntryId { get; set; }
        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }

        public int LocationId { get; set; }
        public DateTime EntryDate { get; set; }
        public long EntryByPersonId { get; set; }
        public string Comments { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Quantity { get; set; }
        public int? StockEntryTypeId { get; set; }
        public int? CountryId { get; set; } //new 24Aug21 prj

        [JsonIgnore]
        public int InventoryEntrySubmittedStatus { get; set; }
    }
}

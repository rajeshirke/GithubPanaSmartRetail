using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class InventoryStockEntry
    {
        public long InventoryStockEntryId { get; set; }
        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public int LocationId { get; set; }
        public DateTime EntryDate { get; set; }
        public long EntryByPersonId { get; set; }
        public string Comments { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Quantity { get; set; }
        public int? EntryTypeId { get; set; }

        public virtual Person EntryByPerson { get; set; }
        public virtual Location Location { get; set; }
    }
}

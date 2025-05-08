using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class InventoryStockEntryResponse
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
        public int? StockEntryTypeId { get; set; }

        public string EntryByPersonFullName
        {
            get { return EntryByPerson.FullName; }
        } 

        public    PersonResponse EntryByPerson { get; set; }
        //public virtual LocationResponse Location { get; set; }
        public   ProductModelResponse ProductModel { get; set; }

        public bool IsQuantityAvailable
        {
            get
            {
                if (StockEntryTypeId == (int)StockEntryTypeEnum.InventoryStockEntry)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsOutofStockEntry
        {
            get
            {
                if (Quantity == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

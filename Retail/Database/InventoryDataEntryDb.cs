using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class InventoryDataEntryDb
    {
        private SQLiteConnection conn;

        public InventoryDataEntryDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblInventoryStockEntryRequest>();
        }

        //READ  
        public IEnumerable<TblInventoryStockEntryRequest> GetInventoryEntries()
        {
            var InventoryEntries = (from mem in conn.Table<TblInventoryStockEntryRequest>() select mem);
            return InventoryEntries.ToList();
        }
        //INSERT  
        public string AddInventoryEntry(TblInventoryStockEntryRequest TblInventoryStockEntryRequest)
        {
            conn.Insert(TblInventoryStockEntryRequest);
            return "success";
        }

        //UPDATE
        public string UpdateInventoryEntries(TblInventoryStockEntryRequest TblInventoryStockEntryRequest)
        {
            conn.Update(TblInventoryStockEntryRequest);
            return "Success";
        }

        //DELETE  
        public string DeleteInventoryEntry(int id)
        {
            conn.Delete<TblInventoryStockEntryRequest>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblInventoryStockEntryRequest>();
            return "Success";
        }

        //READ BASED ON DATE AND STORE
        public IEnumerable<TblInventoryStockEntryRequest> GetInventoryEntrysBasedDateStore(Int32 locationId, DateTime entryDate, int EntryTypeId)
        {
            var InventoryEntrys = (from mem in conn.Table<TblInventoryStockEntryRequest>().Where(p => p.LocationId == locationId && p.EntryDate == entryDate && p.StockEntryTypeId == EntryTypeId) select mem);
            return InventoryEntrys.ToList();
        }

        //DELETE BASED ON DATE AND STORE
        public string DeleteAllEntriesBasedDateStore(Int32 locationId, DateTime entryDate,int EntryTypeId)
        {
            conn.Table<TblInventoryStockEntryRequest>().Delete(p => p.LocationId == locationId && p.EntryDate == entryDate && p.StockEntryTypeId == EntryTypeId);
            return "Success";
        }
    }
}

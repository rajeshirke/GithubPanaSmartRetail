using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class SalesDataEntryDb
    {
        private SQLiteConnection conn;

        //CREATE
        public SalesDataEntryDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TmpSalesDataEntry>();
        }

        //READ  
        public IEnumerable<TmpSalesDataEntry> GetSalesEntrys()
        {
            var salesEntrys = (from mem in conn.Table<TmpSalesDataEntry>() select mem);
            return salesEntrys.ToList();
        }
        //INSERT  
        public string AddSalesEntry(TmpSalesDataEntry TmpSalesDataEntry)
        {
            conn.Insert(TmpSalesDataEntry);
            return "success";
        }
        //DELETE  
        public string DeleteSalesEntry(int id)
        {
            conn.Delete<TmpSalesDataEntry>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TmpSalesDataEntry>();
            return "Success";
        }

        //READ BASED ON DATE AND STORE
        public IEnumerable<TmpSalesDataEntry> GetSalesEntrysBasedDateStore(Int32 locationId, DateTime entryDate)
        {
            var salesEntrys = (from mem in conn.Table<TmpSalesDataEntry>().Where(p=>p.LocationId == locationId && p.EntryDate == entryDate) select mem);
            return salesEntrys.ToList();
        }

        //DELETE BASED ON DATE AND STORE
        public string DeleteAllEntriesBasedDateStore(Int32 locationId, DateTime entryDate)
        {
            conn.Table<TmpSalesDataEntry>().Delete(p => p.LocationId == locationId && p.EntryDate == entryDate);
            return "Success";
        }
    }
}

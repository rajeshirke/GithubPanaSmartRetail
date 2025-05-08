using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class RecentlyModelStoreDb
    {
        private SQLiteConnection conn;

        //CREATE
        public RecentlyModelStoreDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblRecentlyUsedModel>();
        }

        //READ  
        public IEnumerable<string> GetRecentlyModels()
        {
            var salesEntrys = (from mem in conn.Table<TblRecentlyUsedModel>() select mem?.Name);
            return salesEntrys.ToList();
        }
        //INSERT  
        public string AddRecentlyModel(TblRecentlyUsedModel TblRecentlyUsedModel)
        {
            conn.Insert(TblRecentlyUsedModel);
            return "success";
        }
        //DELETE  
        public string DeleteRecentlyModel(int id)
        {
            conn.Delete<TblRecentlyUsedModel>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblRecentlyUsedModel>();
            return "Success";
        }

        ////READ BASED ON DATE AND STORE
        //public IEnumerable<TblRecentlyUsedModel> GetRecentlyModelsBasedDateStore(Int32 locationId, DateTime entryDate)
        //{
        //    var salesEntrys = (from mem in conn.Table<TblRecentlyUsedModel>().Where(p => p.LocationId == locationId && p.EntryDate == entryDate) select mem);
        //    return salesEntrys.ToList();
        //}

        ////DELETE BASED ON DATE AND STORE
        //public string DeleteAllEntriesBasedDateStore(Int32 locationId, DateTime entryDate)
        //{
        //    conn.Table<TblRecentlyUsedModel>().Delete(p => p.LocationId == locationId && p.EntryDate == entryDate);
        //    return "Success";
        //}
    }
}

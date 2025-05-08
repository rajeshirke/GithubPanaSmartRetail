using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class LocationStoreDb
    {
        private SQLiteConnection conn;

        //CREATE
        public LocationStoreDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblLocationStore>();
        }

        //READ  
        public IEnumerable<TblLocationStore> GetlocationStores()
        {
            var locationStores = (from mem in conn.Table<TblLocationStore>() select mem);
            return locationStores.ToList();
        }
        //INSERT  
        public string AddlocationStore(TblLocationStore TblLocationStore)
        {
            conn.Insert(TblLocationStore);
            return "success";
        }
        //DELETE  
        public string DeletelocationStore(int id)
        {
            conn.Delete<TblLocationStore>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblLocationStore>();
            return "Success";
        }

        ////READ BASED ON DATE AND STORE
        //public IEnumerable<TblLocationStore> GetlocationStoresBasedDateStore(Int32 locationId, DateTime entryDate)
        //{
        //    var locationStores = (from mem in conn.Table<TblLocationStore>().Where(p => p.LocationId == locationId && p.EntryDate == entryDate) select mem);
        //    return locationStores.ToList();
        //}

        ////DELETE BASED ON DATE AND STORE
        //public string DeleteAllEntriesBasedDateStore(Int32 locationId, DateTime entryDate)
        //{
        //    conn.Table<TblLocationStore>().Delete(p => p.LocationId == locationId && p.EntryDate == entryDate);
        //    return "Success";
        //}
    }
}

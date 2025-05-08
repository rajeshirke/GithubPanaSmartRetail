using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class PromoterLocationStoreDb
    {
        private SQLiteConnection conn;

        //CREATE
        public PromoterLocationStoreDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblPromoterLocationResponse>();
        }

        //READ  
        public IEnumerable<TblPromoterLocationResponse> GetlocationStores()
        {
            var locationStores = (from mem in conn.Table<TblPromoterLocationResponse>() select mem);
            return locationStores.ToList();
        }
        //INSERT  
        public string AddlocationStore(TblPromoterLocationResponse TblPromoterLocationResponse)
        {
            conn.Insert(TblPromoterLocationResponse);
            return "success";
        }
        //DELETE  
        public string DeletelocationStore(int id)
        {
            conn.Delete<TblPromoterLocationResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblPromoterLocationResponse>();
            return "Success";
        }
    }
}

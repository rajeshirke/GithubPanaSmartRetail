using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Infrastructure.ResponseModels;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class SupLocationStoreDb
    {
        private SQLiteConnection conn;

        //CREATE
        public SupLocationStoreDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblLocationResponse>();
        }

        //READ  
        public IEnumerable<TblLocationResponse> GetlocationStores()
        {
            var locationStores = (from mem in conn.Table<TblLocationResponse>() select mem);
            return locationStores.ToList();
        }
        //READ  
        public IEnumerable<TblLocationResponse> GetlocationStoresByLocationId(long LocationId)
        {
            var visitLocationTasksByVisit = new List<TblLocationResponse>();
            string query = "select * From TblLocationResponse where LocationId = " + LocationId;
            var result = conn.Query<TblLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }
        //INSERT  
        public string AddlocationStore(TblLocationResponse TblLocationResponse)
        {
            conn.Insert(TblLocationResponse);
            return "success";
        }
        //DELETE  
        public string DeletelocationStore(int id)
        {
            conn.Delete<TblLocationResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblLocationResponse>();
            return "Success";
        }
    }
}

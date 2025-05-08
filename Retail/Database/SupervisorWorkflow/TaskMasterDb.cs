using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class TaskMasterDb
    {
        private SQLiteConnection conn;

        //CREATE
        public TaskMasterDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblTaskMasterResponse>();
        }

        //READ  
        public IEnumerable<TblTaskMasterResponse> GettaskMasters()
        {
            var taskMasters = (from mem in conn.Table<TblTaskMasterResponse>() select mem);
            return taskMasters.ToList();
        }
        //READ  
        public IEnumerable<TblTaskMasterResponse> GettaskMastersByTaskMasterId(long TaskMasterId)
        {
            var taskMasters = new List<TblTaskMasterResponse>();
            string query = "select * From TblTaskMasterResponse where TaskMasterId = " + TaskMasterId;
            var result = conn.Query<TblTaskMasterResponse>(query);
            if (result != null && result.Count > 0)
                taskMasters = result;
            return taskMasters.ToList();
        }
        //INSERT  
        public string AddtaskMaster(TblTaskMasterResponse TblTaskMasterResponse)
        {
            conn.Insert(TblTaskMasterResponse);
            return "success";
        }
        //DELETE  
        public string DeletetaskMaster(int id)
        {
            conn.Delete<TblTaskMasterResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblTaskMasterResponse>();
            return "Success";
        }
    }




    /// <summary>
    /// Store, Task linking basedon Accountid
    /// </summary>





    //linking Store and Task based on Account
    public class TaskMasterStoreLinkAccountDb
    {
        private SQLiteConnection conn;

        //CREATE
        public TaskMasterStoreLinkAccountDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblTaskMasterStoreLinkAccount>();
        }

        //READ  
        public IEnumerable<TblTaskMasterStoreLinkAccount> GettaskMasterStoreLinkAccounts()
        {
            var taskMasters = (from mem in conn.Table<TblTaskMasterStoreLinkAccount>() select mem);
            return taskMasters.ToList();
        }
        //READ  
        public IEnumerable<TblTaskMasterStoreLinkAccount> GettaskMasterStoreLinkAccountsByAccountIdAndLocationId(long AccountId, long LocationId)
        {
            var taskMasters = new List<TblTaskMasterStoreLinkAccount>();
            string query = "select * From TblTaskMasterStoreLinkAccount where AccountId = " + AccountId + " and LocationId = " + LocationId;
            var result = conn.Query<TblTaskMasterStoreLinkAccount>(query);
            if (result != null && result.Count > 0)
                taskMasters = result;
            return taskMasters.ToList();
        }
        //READ  
        public IEnumerable<TblTaskMasterStoreLinkAccount> GettaskMasterStoreLinkAccountsByAccountId(long AccountId)
        {
            var taskMasters = new List<TblTaskMasterStoreLinkAccount>();
            string query = "select * From TblTaskMasterStoreLinkAccount where AccountId = " + AccountId;
            var result = conn.Query<TblTaskMasterStoreLinkAccount>(query);
            if (result != null && result.Count > 0)
                taskMasters = result;
            return taskMasters.ToList();
        }
        //INSERT  
        public string AddtaskMasterStoreLinkAccount(TblTaskMasterStoreLinkAccount TblTaskMasterStoreLinkAccount)
        {
            conn.Insert(TblTaskMasterStoreLinkAccount);
            return "success";
        }
        //DELETE  
        public string DeletetaskMasterStoreLinkAccount(int id)
        {
            conn.Delete<TblTaskMasterStoreLinkAccount>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblTaskMasterStoreLinkAccount>();
            return "Success";
        }
    }
}

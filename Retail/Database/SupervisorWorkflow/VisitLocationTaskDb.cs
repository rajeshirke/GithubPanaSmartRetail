using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class VisitLocationTaskDb
    {
        private SQLiteConnection conn;

        //CREATE
        public VisitLocationTaskDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblVisitLocationTaskResponse>();
        }

        //READ  
        public IEnumerable<TblVisitLocationTaskResponse> GetvisitLocationTasks()
        {
            var visitLocationTasks = (from mem in conn.Table<TblVisitLocationTaskResponse>() select mem);
            return visitLocationTasks.ToList();
        }

        //READ  
        public IEnumerable<TblVisitLocationTaskResponse> GetvisitLocationTasksByPVisitLocationTaskId(long PVisitLocationTaskId)
        {
            var visitLocationTasksByVisit = new List<TblVisitLocationTaskResponse>();
            string query = "select * From TblVisitLocationTaskResponse where PVisitLocationTaskId = " + PVisitLocationTaskId;
            var result = conn.Query<TblVisitLocationTaskResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        //READ  
        public IEnumerable<TblVisitLocationTaskResponse> GetvisitLocationTasksByPVisitScheduleLocationIdAndVisitDate(long PVisitScheduleLocationId, string VisitDate)
        {
            var visitLocationTasksByVisit = new List<TblVisitLocationTaskResponse>();
            string query = "select * From TblVisitLocationTaskResponse where PVisitScheduleLocationId = " + PVisitScheduleLocationId + " and CreatedDate='" + VisitDate + "'";
            var result = conn.Query<TblVisitLocationTaskResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        //READ  
        public IEnumerable<TblVisitLocationTaskResponse> GetvisitLocationTasksByPVisitScheduleLocationId(long PVisitScheduleLocationId)
        {
            var visitLocationTasksByVisit = new List<TblVisitLocationTaskResponse>();
            string query = "select * From TblVisitLocationTaskResponse where PVisitScheduleLocationId = " + PVisitScheduleLocationId;
            var result = conn.Query<TblVisitLocationTaskResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        //UPDATE  
        public IEnumerable<TblVisitLocationTaskResponse> UpdatevisitLocationTaskStatusByPVisitLocationTaskId(long PVisitLocationTaskId, int TaskStatusId)
        {
            long? TaskCompletionDate = null;
            if (TaskStatusId == (int)TaskStatusEnum.Closed)
                TaskCompletionDate = DateTime.Now.Ticks;

            string query = "";
            var visitLocationTasksByVisit = new List<TblVisitLocationTaskResponse>();
            if (TaskCompletionDate != null)
                query = "update TblVisitLocationTaskResponse set TaskStatusId=" + TaskStatusId + ",TaskCompletionDate='" + TaskCompletionDate + "' where PVisitLocationTaskId = " + PVisitLocationTaskId;
            else
                query = "update TblVisitLocationTaskResponse set TaskStatusId=" + TaskStatusId + " where PVisitLocationTaskId = " + PVisitLocationTaskId;

            var result = conn.Query<TblVisitLocationTaskResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        //INSERT  
        public string AddvisitLocationTask(TblVisitLocationTaskResponse TblVisitLocationTaskResponse)
        {
            conn.Insert(TblVisitLocationTaskResponse);
            return "success";
        }


        //DELETE  
        public string DeletevisitLocationTask(int id)
        {
            conn.Delete<TblVisitLocationTaskResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblVisitLocationTaskResponse>();
            return "Success";
        }
    }
}

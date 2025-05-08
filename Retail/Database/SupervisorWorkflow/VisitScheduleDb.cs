using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class VisitScheduleDb
    {
        private SQLiteConnection conn;

        //CREATE
        public VisitScheduleDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblVisitScheduleResponse>();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitSchedules()
        {
            var visitSchedules = (from mem in conn.Table<TblVisitScheduleResponse>() select mem);
            return visitSchedules.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByVisitScheduleStatusId(long VisitScheduleStatusId)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where VisitScheduleStatusId=" + VisitScheduleStatusId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByVisitScheduleNotInStatusId(long VisitScheduleStatusId, string VisitDate)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where CreationDate='" + VisitDate + "' and VisitScheduleStatusId <>" + VisitScheduleStatusId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByVisitScheduleAllToday(string VisitDate)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where CreationDate='" + VisitDate + "'";
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByVisitScheduleNotInStatusIdsGetTop1(long VisitScheduleStatusId)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where VisitScheduleStatusId <>" + VisitScheduleStatusId +" order by PVisitScheduleId desc limit 1";
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }


        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByPVisitScheduleId(long PVisitScheduleId)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where PVisitScheduleId=" + PVisitScheduleId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleResponse> GetvisitScheduleByCreationDate(string CreationDate)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * From TblVisitScheduleResponse where CreationDate='" + CreationDate + "'";
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //INSERT  
        public string AddvisitSchedule(TblVisitScheduleResponse TblVisitScheduleResponse)
        {
            conn.Insert(TblVisitScheduleResponse);
            return "success";
        }

        //UPDATE
        public IEnumerable<TblVisitScheduleResponse> UpdateVisitScheduleStatusByPVisitScheduleId(long PVisitScheduleId, long TaskStatusId)
        {
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "update TblVisitScheduleResponse set VisitScheduleStatusId=" + TaskStatusId + " where PVisitScheduleId = " + PVisitScheduleId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //UPDATE  
        public IEnumerable<TblVisitScheduleResponse> updateVisitCheduleLocationStatus(long PVisitScheduleId, List<TblVisitLocationTaskResponse> tblVisitLocationTaskResponses)
        {
            int StatusId = (int)TaskStatusEnum.Open;
            if (tblVisitLocationTaskResponses != null && tblVisitLocationTaskResponses.Count() > 0)
            {
                int j = -1;
                foreach (var itemj in tblVisitLocationTaskResponses)
                {
                    j = j + 1;
                    // if not matching then inprogress
                    if (j > 0 && StatusId != itemj.TaskStatusId)
                    {
                        StatusId = (int)TaskStatusEnum.InProgress;
                        break;
                    }

                    if (itemj.TaskStatusId == (int)TaskStatusEnum.Open)
                    {
                        StatusId = (int)TaskStatusEnum.Open;
                    }
                    else if (itemj.TaskStatusId == (int)TaskStatusEnum.Closed)
                    {
                        StatusId = (int)TaskStatusEnum.Closed;
                    }
                    else if (itemj.TaskStatusId == (int)TaskStatusEnum.InProgress)
                    {
                        StatusId = (int)TaskStatusEnum.InProgress;
                    }
                }
            }

            var visitLocationTasksByVisit = new List<TblVisitScheduleResponse>();
            string query = "update TblVisitScheduleResponse set StatusId=" + StatusId + " where PVisitScheduleId = " + PVisitScheduleId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        //INSERT  
        public long AddvisitScheduleQuery(TblVisitScheduleResponse TblVisitScheduleResponse, long VisitScheduleStatusId)
        {
            long primaryKey = 0;

            //check already visit schedule on this date exists
            //string query = "select * From TblVisitScheduleResponse where CreationDate='" + TblVisitScheduleResponse.CreationDate + "' and VisitScheduleStatusId <>" + VisitScheduleStatusId;
            string query = "select * From TblVisitScheduleResponse where VisitScheduleStatusId =" + VisitScheduleStatusId;

            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count == 0)
                conn.Insert(TblVisitScheduleResponse);

            // getting primary key
            string query1 = "select * From TblVisitScheduleResponse where CreationDate='" + TblVisitScheduleResponse.CreationDate + "' order by PVisitScheduleId desc limit 1";
            var result1 = conn.Query<TblVisitScheduleResponse>(query1);
            if (result1 != null && result1.Count > 0)
                primaryKey = result1[0].PVisitScheduleId;
            return primaryKey;
            //return "success";
        }

        //DELETE  
        public string DeletevisitSchedule(int id)
        {
            conn.Delete<TblVisitScheduleResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblVisitScheduleResponse>();
            return "Success";
        }


        //DELETE ALL PREVIOUS SCHEDULES NOT SUBMITTED
        public IEnumerable<TblVisitScheduleResponse> DeletePreviousVisitScheduleStatusByNotInVisitScheduleStatusId(long VisitScheduleStatusId)
        {
            string TodaysDate = DateTime.Today.ToString("yyyy-MM-dd");
            var visitSchedules = new List<TblVisitScheduleResponse>();
            string query = "select * from TblVisitScheduleResponse where CreationDate <> '" + TodaysDate + "' and VisitScheduleStatusId <>" + VisitScheduleStatusId;
            var result = conn.Query<TblVisitScheduleResponse>(query);
            if (result != null && result.Count > 0)
            {
                visitSchedules = result;
                foreach (var item in visitSchedules)
                {
                    string query1 = "delete from TblVisitScheduleResponse where PVisitScheduleId = " + item.PVisitScheduleId;
                    var result1 = conn.Query<TblVisitScheduleResponse>(query1);
                    string query2 = "delete from TblVisitScheduleLocationResponse where PVisitScheduleId = " + item.PVisitScheduleId;
                    var result2 = conn.Query<TblVisitScheduleLocationResponse>(query2);
                }
            }
            return visitSchedules.ToList();
        }
    }
}

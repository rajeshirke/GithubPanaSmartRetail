using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class TaskQuestionAnswerDb
    {
        private SQLiteConnection conn;

        //CREATE
        public TaskQuestionAnswerDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblTaskQuestionAnswerResponse>();
        }

        //READ  
        public IEnumerable<TblTaskQuestionAnswerResponse> GettaskQuestionAnswers()
        {
            var taskQuestionAnswers = (from mem in conn.Table<TblTaskQuestionAnswerResponse>() select mem);
            return taskQuestionAnswers.ToList();
        }

        //READ  
        public IEnumerable<TblTaskQuestionAnswerResponse> GettaskQuestionAnswerByQuestionMasterId(long QuestionMasterId, long PVisitLocationTaskId)
        {
            var taskQuestionAnswers = new List<TblTaskQuestionAnswerResponse>();
            string query = "select * From TblTaskQuestionAnswerResponse where QuestionMasterId = " + QuestionMasterId + " and PVisitLocationTaskId = " + PVisitLocationTaskId;
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            if (result != null && result.Count > 0)
                taskQuestionAnswers = result;
            return taskQuestionAnswers.ToList();
        }

        //READ  
        public IEnumerable<TblTaskQuestionAnswerResponse> GettaskQuestionAnswerByVisitLocationTaskMasterId(long PVisitLocationTaskId)
        {
            var taskQuestionAnswers = new List<TblTaskQuestionAnswerResponse>();
            string query = "select * From TblTaskQuestionAnswerResponse where PVisitLocationTaskId = " + PVisitLocationTaskId;
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            if (result != null && result.Count > 0)
                taskQuestionAnswers = result;
            return taskQuestionAnswers.ToList();
        }

        //READ  
        public IEnumerable<TblTaskQuestionAnswerResponse> GettaskQuestionAnswerByTaskMasterId(long TaskMasterId)
        {
            var taskQuestionAnswers = new List<TblTaskQuestionAnswerResponse>();
            string query = "select * From TblTaskQuestionAnswerResponse where TaskMasterId = " + TaskMasterId;
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            if (result != null && result.Count > 0)
                taskQuestionAnswers = result;
            return taskQuestionAnswers.ToList();
        }

        //INSERT  
        public long AddtaskQuestionAnswer(TblTaskQuestionAnswerResponse TblTaskQuestionAnswerResponse)
        {
            long primaryKey = 0;
            conn.Insert(TblTaskQuestionAnswerResponse);
            string query = "select * From TblTaskQuestionAnswerResponse order by PTaskQuestionAnswerId desc limit 1";
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            if (result != null && result.Count > 0)
                primaryKey = result[0].PTaskQuestionAnswerId;
            return primaryKey;
        }

        //DELETE  
        public string DeletetaskQuestionAnswer(int id)
        {
            conn.Delete<TblTaskQuestionAnswerResponse>(id);
            return "success";
        }

        //DELETE  
        public string DeletetaskQuestionAnswer(long QuestionMasterId)
        {
            var visitLocationTasksByVisit = new List<TblTaskQuestionAnswerResponse>();
            string query = "delete From TblTaskQuestionAnswerResponse where QuestionMasterId = " + QuestionMasterId;
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            return "success";
        }

        //DELETE  
        public string DeletetaskQuestionAnswerByPVisitLocationTaskId(long PVisitLocationTaskId)
        {
            var visitLocationTasksByVisit = new List<TblTaskQuestionAnswerResponse>();
            string query = "delete From TblTaskQuestionAnswerResponse where PVisitLocationTaskId = " + PVisitLocationTaskId;
            var result = conn.Query<TblTaskQuestionAnswerResponse>(query);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblTaskQuestionAnswerResponse>();
            return "Success";
        }
    }
}

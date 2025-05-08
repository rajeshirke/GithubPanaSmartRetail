using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class TaskMasterQuestionDb
    {
        private SQLiteConnection conn;

        //CREATE
        public TaskMasterQuestionDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblTaskMasterQuestionResponse>();
        }

        //READ  
        public IEnumerable<TblTaskMasterQuestionResponse> GettaskMasterQuestions()
        {
            var taskMasterQuestions = (from mem in conn.Table<TblTaskMasterQuestionResponse>() select mem);
            return taskMasterQuestions.ToList();
        }

        //READ  
        public IEnumerable<TblTaskMasterQuestionResponse> GettaskMasterQuestionByTaskMasterId(long TaskMasterId)
        {
            var taskMasterQuestions = new List<TblTaskMasterQuestionResponse>();
            string query = "select * From TblTaskMasterQuestionResponse where TaskMasterId = " + TaskMasterId;
            var result = conn.Query<TblTaskMasterQuestionResponse>(query);
            if (result != null && result.Count > 0)
                taskMasterQuestions = result;
            return taskMasterQuestions.ToList();
        }

        //INSERT  
        public string AddtaskMasterQuestion(TblTaskMasterQuestionResponse TblTaskMasterQuestionResponse)
        {
            conn.Insert(TblTaskMasterQuestionResponse);
            return "success";
        }
        //DELETE  
        public string DeletetaskMasterQuestion(int id)
        {
            conn.Delete<TblTaskMasterQuestionResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblTaskMasterQuestionResponse>();
            return "Success";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class AnswerOptionDb
    {
        private SQLiteConnection conn;

        //CREATE
        public AnswerOptionDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblAnswerOptionResponse>();
        }

        //READ  
        public IEnumerable<TblAnswerOptionResponse> GetanswerOptions()
        {
            var answerOptions = (from mem in conn.Table<TblAnswerOptionResponse>() select mem);
            return answerOptions.ToList();
        }

        //READ  
        public IEnumerable<TblAnswerOptionResponse> GetanswerOptionByQuestionMasterId(long QuestionMasterId)
        {
            var answerOptions = new List<TblAnswerOptionResponse>();
            string query = "select * From TblAnswerOptionResponse where QuestionMasterId = " + QuestionMasterId;
            var result = conn.Query<TblAnswerOptionResponse>(query);
            if (result != null && result.Count > 0)
                answerOptions = result;
            return answerOptions.ToList();
        }

        //READ  
        //public IEnumerable<TblAnswerOptionResponse> GetanswerOptionByPTaskQuestionAnswerId(long PTaskQuestionAnswerId)
        //{
        //    var answerOptions = new List<TblAnswerOptionResponse>();
        //    string query = "select * From TblAnswerOptionResponse where PTaskQuestionAnswerId = " + PTaskQuestionAnswerId;
        //    var result = conn.Query<TblAnswerOptionResponse>(query);
        //    if (result != null && result.Count > 0)
        //        answerOptions = result;
        //    return answerOptions.ToList();
        //}

        //INSERT  
        public string AddanswerOption(TblAnswerOptionResponse TblAnswerOptionResponse)
        {
            conn.Insert(TblAnswerOptionResponse);
            return "success";
        }
        //DELETE  
        public string DeleteanswerOption(int id)
        {
            conn.Delete<TblAnswerOptionResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblAnswerOptionResponse>();
            return "Success";
        }
    }
}

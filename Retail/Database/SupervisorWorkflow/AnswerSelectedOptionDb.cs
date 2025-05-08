using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class AnswerSelectedOptionDb
    {
        private SQLiteConnection conn;

        //CREATE
        public AnswerSelectedOptionDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblAnswerSelectedOptionResponse>();
        }

        //READ  
        public IEnumerable<TblAnswerSelectedOptionResponse> GetanswerSelectedOptions()
        {
            var answerSelectedOptions = (from mem in conn.Table<TblAnswerSelectedOptionResponse>() select mem);
            return answerSelectedOptions.ToList();
        }

        //READ  
        public IEnumerable<TblAnswerSelectedOptionResponse> GetanswerSelectedOptionByQuestionMasterId(long QuestionMasterId)
        {
            var answerSelectedOptions = new List<TblAnswerSelectedOptionResponse>();
            string query = "select * From TblAnswerSelectedOptionResponse where QuestionMasterId = " + QuestionMasterId;
            var result = conn.Query<TblAnswerSelectedOptionResponse>(query);
            if (result != null && result.Count > 0)
                answerSelectedOptions = result;
            return answerSelectedOptions.ToList();
        }

        //READ  
        public IEnumerable<TblAnswerSelectedOptionResponse> GetanswerSelectedOptionByPTaskQuestionAnswerId(long PTaskQuestionAnswerId)
        {
            var answerSelectedOptions = new List<TblAnswerSelectedOptionResponse>();
            string query = "select * From TblAnswerSelectedOptionResponse where PTaskQuestionAnswerId = " + PTaskQuestionAnswerId;
            var result = conn.Query<TblAnswerSelectedOptionResponse>(query);
            if (result != null && result.Count > 0)
                answerSelectedOptions = result;
            return answerSelectedOptions.ToList();
        }

        //INSERT  
        public string AddanswerSelectedOption(TblAnswerSelectedOptionResponse TblAnswerSelectedOptionResponse)
        {
            conn.Insert(TblAnswerSelectedOptionResponse);
            return "success";
        }
        //DELETE  
        public string DeleteanswerSelectedOption(int id)
        {
            conn.Delete<TblAnswerSelectedOptionResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblAnswerSelectedOptionResponse>();
            return "Success";
        }
    }
}

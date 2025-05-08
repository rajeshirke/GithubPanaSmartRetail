using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class QuestionMasterDb
    {
        private SQLiteConnection conn;

        //CREATE
        public QuestionMasterDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblQuestionMasterResponse>();
        }

        //READ  
        public IEnumerable<TblQuestionMasterResponse> GetquestionMasters()
        {
            var questionMasters = (from mem in conn.Table<TblQuestionMasterResponse>() select mem);
            return questionMasters.ToList();
        }

        //READ  
        public IEnumerable<TblQuestionMasterResponse> GetquestionMastersByQuestionMasterId(long QuestionMasterId)
        {
            var questionMasters = new List<TblQuestionMasterResponse>();
            string query = "select * From TblQuestionMasterResponse where QuestionMasterId = " + QuestionMasterId;
            var result = conn.Query<TblQuestionMasterResponse>(query);
            if (result != null && result.Count > 0)
                questionMasters = result;
            return questionMasters.ToList();
        }

        //INSERT  
        public string AddquestionMaster(TblQuestionMasterResponse TblQuestionMasterResponse)
        {
            conn.Insert(TblQuestionMasterResponse);
            return "success";
        }
        //DELETE  
        public string DeletequestionMaster(int id)
        {
            conn.Delete<TblQuestionMasterResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblQuestionMasterResponse>();
            return "Success";
        }
    }
}

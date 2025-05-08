using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class AnswerUploadedFileDb
    {
        private SQLiteConnection conn;

        //CREATE
        public AnswerUploadedFileDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblAnswerUploadedFileResponse>();
        }

        //READ  
        public IEnumerable<TblAnswerUploadedFileResponse> GetanswerUploaedFiles()
        {
            var answerUploaedFiles = (from mem in conn.Table<TblAnswerUploadedFileResponse>() select mem);
            return answerUploaedFiles.ToList();
        }

        //READ  
        public IEnumerable<TblAnswerUploadedFileResponse> GetanswerUploaedFileByPTaskQuestionAnswerId(long PTaskQuestionAnswerId)
        {
            var answerUploaedFiles = new List<TblAnswerUploadedFileResponse>();
            string query = "select * From TblAnswerUploadedFileResponse where PTaskQuestionAnswerId = " + PTaskQuestionAnswerId;
            var result = conn.Query<TblAnswerUploadedFileResponse>(query);
            if (result != null && result.Count > 0)
                answerUploaedFiles = result;
            return answerUploaedFiles.ToList();
        }

        //INSERT  
        public string AddanswerUploaedFile(TblAnswerUploadedFileResponse TblAnswerUploadedFileResponse)
        {
            conn.Insert(TblAnswerUploadedFileResponse);
            return "success";
        }
        //DELETE  
        public string DeleteanswerUploaedFile(int id)
        {
            conn.Delete<TblAnswerUploadedFileResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblAnswerUploadedFileResponse>();
            return "Success";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Retail.DependencyServices;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class FileInfoDb
    {
        private SQLiteConnection conn;

        //CREATE
        public FileInfoDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblFileInfoResponse>();
        }

        //READ  
        public IEnumerable<TblFileInfoResponse> GetfileInfos()
        {
            var fileInfos = (from mem in conn.Table<TblFileInfoResponse>() select mem);
            return fileInfos.ToList();
        }

        //READ  
        public IEnumerable<TblFileInfoResponse> GetfileInfoByPFileInfoId(long PFileInfoId)
        {
            var fileInfos = new List<TblFileInfoResponse>();
            string query = "select * From TblFileInfoResponse where PFileInfoId = " + PFileInfoId;
            var result = conn.Query<TblFileInfoResponse>(query);
            if (result != null && result.Count > 0)
                fileInfos = result;
            return fileInfos.ToList();
        }

        //INSERT  
        public long AddfileInfo(TblFileInfoResponse TblFileInfoResponse)
        {
            long primaryKey = 0;
            conn.Insert(TblFileInfoResponse);
            string query = "select * From TblFileInfoResponse order by PFileInfoId desc limit 1";
            var result = conn.Query<TblFileInfoResponse>(query);
            if (result != null && result.Count > 0)
                primaryKey = result[0].PFileInfoId;
            return primaryKey;
            //return "success";
        }
        //DELETE  
        public string DeletefileInfo(int id)
        {
            conn.Delete<TblFileInfoResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblFileInfoResponse>();
            return "Success";
        }
    }
}

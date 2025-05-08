using System;
using System.Diagnostics;
using System.IO;
using Android.App;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace Retail.Droid.DependencyServices
{
    public class SQLiteDb: ISQLite
    {
        public string PathToDb = "";
        public string dbName = "RMD_db.db3";

        public SQLite.SQLiteConnection GetConnection()
        {
            var conn = new SQLite.SQLiteConnection("");
            try
            {
                var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                PathToDb = System.IO.Path.Combine(dbPath, dbName);
                conn = new SQLite.SQLiteConnection(PathToDb);
            }
            catch (Exception ex)
            {
                Debugger.Log(0,null,ex.ToString());
            }
            return conn;
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace Retail.iOS.DependencyServices
{
    public class SQLiteDb:ISQLite
    {
        public string PathToDb = "";
        public string dbName = "RMD_db.db3";

        public SQLite.SQLiteConnection GetConnection()
        {
            var conn = new SQLite.SQLiteConnection("");
            try
            {
                string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Documents folder  
                                                                                                      //string libraryPath = Path.Combine(dbPath, ); // Library folder  
                PathToDb = Path.Combine(dbPath, dbName);
                conn = new SQLite.SQLiteConnection(PathToDb);
            }
            catch(Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            return conn;
        }
    }
}

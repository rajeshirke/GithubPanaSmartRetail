using System;
using SQLite;

namespace Retail.DependencyServices
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
        //SQLiteAsyncConnection GetConnection();
    }
}
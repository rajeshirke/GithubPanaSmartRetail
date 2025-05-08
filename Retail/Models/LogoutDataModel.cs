using System;
namespace Retail.Models
{
    public class LogoutDataModel
    {
        static LogoutDataModel()
        {

        }

        public static int LogoutUser = 0;
    }

    public enum LogoutDataEnum
    {
       ManualLogout = 1,
       SessionTimeout,
       ConcurrencyTimeout,
       SqlLiteConnectionIssue
    }
}

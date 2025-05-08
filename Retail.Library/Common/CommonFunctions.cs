using System;
namespace Retail.Infrastructure.Common
{
    public static class CommonFunctions
    {
        public static string GetDMYFormat(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }
        public static string GetYMDFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
        public static string GetDMFormat(DateTime date)
        {
            return date.ToString("dd-MMM");
        }
        public static string GetTimeFormat(DateTime date)
        {
            return date.ToString("HH:mm");
        }
        public static DateTime GetDatePartOnly(DateTime date)
        {
            return Convert.ToDateTime(date.ToShortDateString());
        }
    }
}

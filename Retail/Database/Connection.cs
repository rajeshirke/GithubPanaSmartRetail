using System;
using System.IO;
using Retail.DependencyServices;
using Retail.Models;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database
{
    public class Connection
    {
        public SQLiteConnection conn;

        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

        public void con()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TmpSalesDataEntry>();
            conn.CreateTable<TblPromoterAttendance>();
            conn.CreateTable<TblInventoryStockEntryRequest>();
            conn.CreateTable<TblRecentlyUsedModel>();
            conn.CreateTable<TblProductCategories>();
            conn.CreateTable<TblSubcategories>();
            //table creation for supervisor
            conn.CreateTable<TblLocationResponse>();
            conn.CreateTable<TblVisitScheduleLocationResponse>();
            conn.CreateTable<TblVisitLocationTaskResponse>();
            conn.CreateTable<TblTaskMasterStoreLinkAccount>();
            conn.CreateTable<TblTaskMasterResponse>();
            conn.CreateTable<TblTaskQuestionAnswerResponse>();
            conn.CreateTable<TblTaskMasterQuestionResponse>();
            conn.CreateTable<TblQuestionMasterResponse>();
            conn.CreateTable<TblAnswerOptionResponse>();
            conn.CreateTable<TblAnswerSelectedOptionResponse>();
            conn.CreateTable<TblAnswerUploadedFileResponse>();
            conn.CreateTable<TblFileInfoResponse>();
        }

        public void clear()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.DropTable<TmpSalesDataEntry>();
            conn.DropTable<TblPromoterAttendance>();
            conn.DropTable<TblInventoryStockEntryRequest>();
            conn.DropTable<TblAnswerOptionResponse>();
            conn.DropTable<TblAnswerSelectedOptionResponse>();
            conn.DropTable<TblAnswerUploadedFileResponse>();
            conn.DropTable<TblRecentlyUsedModel>();
            conn.DropTable<TblProductCategories>();
            conn.DropTable<TblSubcategories>();
            conn.DropTable<TblFileInfoResponse>();
            conn.DropTable<TblLocationStore>();
            conn.DropTable<TblAllModelNumbers>();
            conn.DropTable<TblPromoterLocationResponse>();
            conn.DropTable<TblQuestionMasterResponse>();
            conn.DropTable<TblLocationResponse>();
            conn.DropTable<TblVisitScheduleLocationResponse>();
            conn.DropTable<TblVisitLocationTaskResponse>();
            conn.DropTable<TblTaskMasterStoreLinkAccount>();
            conn.DropTable<TblTaskMasterResponse>();
            conn.DropTable<TblTaskQuestionAnswerResponse>();
            conn.DropTable<TblTaskMasterQuestionResponse>();
            conn.DropTable<TblVisitScheduleResponse>();
        }

        public void NoClearSupData()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
                   
            conn.DropTable<TblRecentlyUsedModel>();
            conn.DropTable<TblProductCategories>();
            conn.DropTable<TblSubcategories>();           
            conn.DropTable<TblLocationStore>();
            conn.DropTable<TblAllModelNumbers>();
            conn.DropTable<TblPromoterLocationResponse>();            
            conn.DropTable<TblLocationResponse>();

            //******** For not to clear Promoter data*********
            //conn.DropTable<TmpSalesDataEntry>();
            //conn.DropTable<TblPromoterAttendance>();
            //conn.DropTable<TblInventoryStockEntryRequest>();

            //*********For not to clear supervisor visits data********
            //conn.DropTable<TblTaskMasterStoreLinkAccount>();
            //conn.DropTable<TblVisitLocationTaskResponse>();
            //conn.DropTable<TblQuestionMasterResponse>();
            //conn.DropTable<TblVisitScheduleLocationResponse>();
            //conn.DropTable<TblFileInfoResponse>();
            //conn.DropTable<TblTaskMasterResponse>();
            //conn.DropTable<TblTaskQuestionAnswerResponse>();
            //conn.DropTable<TblTaskMasterQuestionResponse>();
            //conn.DropTable<TblVisitScheduleResponse>();
            //conn.DropTable<TblAnswerOptionResponse>();
            //conn.DropTable<TblAnswerSelectedOptionResponse>();
            //conn.DropTable<TblAnswerUploadedFileResponse>();
        }

    }
}

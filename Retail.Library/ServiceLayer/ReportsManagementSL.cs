using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using APIResponse = Retail.Infrastructure.RequestModels.APIResponse;

namespace Retail.Infrastructure.ServiceLayer
{
    public class ReportsManagementSL
    {
        //public async Task<List<SalesTargetCountryCityAccountReportView>> GetSalesAchievedvsCountry(long countryId,string fromDate, long accountId, long cityId) //,
        //{
        //    var result= await WebServiceUtils<List<SalesTargetCountryCityAccountReportView>>.GetData(string.Format(ServiceEndPoints.GetSalesAchievedvsCountry, countryId, fromDate, accountId,cityId)); //,  cityId,  accountId
        //    return result;
        //}

        public async Task<List<SalesTargetCountryCityAccountReportView>> GetSalesAchievedvsCountry(ReportCreateUpdateRequest reportCreateUpdateRequest) //,
        {
            var result = await WebServiceUtils<List<SalesTargetCountryCityAccountReportView>>.PostData(ServiceEndPoints.GetSalesAchievedvsCountry, reportCreateUpdateRequest); //,  cityId,  accountId
            return result;
        }

        //public async Task<List<SalesTargetProductCategoryReportView>> GetSalesAchievedvsProductCategory(long countryId, string fromDate, long cityId, long accountId) //, long cityId, long accountId
        //{
        //    var result= await WebServiceUtils<List<SalesTargetProductCategoryReportView>>.GetData(string.Format(ServiceEndPoints.GetSalesAchievedvsProductCategory, countryId, fromDate, accountId, cityId));//, cityId, accountId
        //    return result;
        //}

        public async Task<List<SalesTargetProductCategoryReportView>> GetSalesAchievedvsProductCategory(ReportCreateUpdateRequest reportCreateUpdateRequest) //, long cityId, long accountId
        {
            var result = await WebServiceUtils<List<SalesTargetProductCategoryReportView>>.PostData(ServiceEndPoints.GetSalesAchievedvsProductCategory, reportCreateUpdateRequest);//, cityId, accountId
            return result;
        }

        public async Task<List<SalesTargetPromoterAchievementReportView>> GetSalesTargetPromoterAchievementReportByCountryAccountLocationMultiple(ReportCreateUpdateRequest reportCreateUpdateRequest) 
        {
            return await WebServiceUtils<List<SalesTargetPromoterAchievementReportView>>.PostData(ServiceEndPoints.GetSalesTargetPromoterAchievementReportByCountryAccountLocationMultiple, reportCreateUpdateRequest); 
        }

        public async Task<List<SalesEntryDailyBySubCategoryView>> GetDailySalesTrendBySubcategoryAll(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            return await WebServiceUtils<List<SalesEntryDailyBySubCategoryView>>.PostData(ServiceEndPoints.GetDailySalesTrendBySubcategoryAll, reportCreateUpdateRequest);
        }

        //public async Task<List<AttendanceView>> GetPromoterAttendance(string SelectedDate)
        //{
        //    return await WebServiceUtils<List<AttendanceView>>.GetData(string.Format(ServiceEndPoints.GetPromoterAttendance, SelectedDate));
        //}

        public async Task<List<AttendanceView>> GetPromoterAttendanceNew(ReportCreateUpdateRequest reportCreateUpdate)
        {
            return await WebServiceUtils<List<AttendanceView>>.PostData(ServiceEndPoints.GetPromoterAttendanceNew, reportCreateUpdate);
        }

        public async Task<List<DailyVisitTargetTasksReportView>> GetDailyVisitTargetTasksReportAll(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            return await WebServiceUtils<List<DailyVisitTargetTasksReportView>>.PostData(ServiceEndPoints.GetDailyVisitTargetTasksReportAll, reportCreateUpdateRequest);
        }

        public async Task<List<SalesEntryReportView>> GetCategoryWiseContribution(long PersonId,string Selecteddate)
        {
            return await WebServiceUtils<List<SalesEntryReportView>>.GetData(string.Format(ServiceEndPoints.GetCategoryWiseContribution, PersonId, Selecteddate));
        }

    }
}

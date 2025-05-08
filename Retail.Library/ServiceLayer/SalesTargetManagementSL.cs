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
    public class SalesTargetManagementSL
    {
        public async Task<SalesTargetWithSummaryResponse> GetSalesTargetWithSummaryByPersonId(int PersonId)
        {
            return await WebServiceUtils<SalesTargetWithSummaryResponse>.GetData(string.Format(ServiceEndPoints.GetSalesTargetWithSummaryByPersonId, PersonId));
        }

        //SaveSalesDataEntry
        public async Task<APIResponse> SaveSalesDataEntries(List<SalesEntryRequest> salesEntryRequests)
        {
            var abc = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveSalesDataEntries, salesEntryRequests);
            return abc;
        }

        //TargetsOverview Supervisor
        public async Task<SupervisorTargetsOverviewResponse> GetSupervisorTargetsOverview(TargetsOverviewByCityStoreRequest targetsOverviewByCityStoreRequests)
        {
            var result= await WebServiceUtils<SupervisorTargetsOverviewResponse>.PostData(ServiceEndPoints.GetSupervisorTargetsOverview, targetsOverviewByCityStoreRequests);
            return result;
        }
        
        //PromoterTargets
        public async Task<PromoterTargetsOverviewResponse> GetPromoterTargets(PromoterDetails promoters)
        {
            var result = await WebServiceUtils<PromoterTargetsOverviewResponse>.PostData(ServiceEndPoints.GetPromoterTargets, promoters);
            return result;
        }

        //RecentlyUsedModelNo
        public async Task<List<string>> GetRecentTargetModelEntriesByPerson(long PersonId)
        {
            var result = await WebServiceUtils<List<string>>.GetData(string.Format(ServiceEndPoints.GetRecentTargetModelEntriesByPerson, PersonId));
            return result;
        }

        //Model nos by category
        public async Task<List<string>> GetOnlyProductModelNumbersActiveByCategoryId(long countryid,long categoryid,long subcategoryid)
        {
            var result = await WebServiceUtils<List<string>>.GetData(string.Format(ServiceEndPoints.GetOnlyProductModelNumbersActiveByCategoryId, countryid, categoryid, subcategoryid));
            return result;
        }

        //ConfirmSalesEntry
        public async Task<List<SalesEntryRequest>> BuildSalesEntryReuqestWithModelDetails(List<SalesEntryRequest> salesEntryRequests)
        {
            var abc = await WebServiceUtils<List<SalesEntryRequest>>.PostData(ServiceEndPoints.BuildSalesEntryReuqestWithModelDetails, salesEntryRequests);
            return abc;
        }

        //SaveSalesReturn
        public async Task<APIResponse> SaveSalesDataReturnEntries(List<SalesEntryRequest> salesEntryRequests)
        {
            var abc = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveSalesDataReturnEntries, salesEntryRequests);
            return abc;
        }

        public async Task<List<SalesEntryRequest>> GetSalesEntryTransactionsByDate(string SalesDate,int EntrystatusId, long PersonId)
        {
            var result = await WebServiceUtils<List<SalesEntryRequest>>.GetData(string.Format(ServiceEndPoints.GetSalesEntryTransactionsByDate, SalesDate, EntrystatusId, PersonId));
            return result;
        }

        public async Task<List<SalesTargetEntryResponse>> GetSalesEntryByPersonIdSubCategoryId(long PersonId,long SubcategoryId,string Fromdate,string Todate)
        {
            var result = await WebServiceUtils<List<SalesTargetEntryResponse>>.GetData(string.Format(ServiceEndPoints.GetSalesEntryByPersonIdSubCategoryId, PersonId, SubcategoryId, Fromdate, Todate));
            return result;
        }

        public async Task<bool> ValidateModelByModelNumberCountry(int CountryId,string ModelNumber)
        {
            var result = await WebServiceUtils<bool>.GetData(string.Format(ServiceEndPoints.ValidateModelByModelNumberCountry, CountryId, ModelNumber));
            return result;
        }

    }

}

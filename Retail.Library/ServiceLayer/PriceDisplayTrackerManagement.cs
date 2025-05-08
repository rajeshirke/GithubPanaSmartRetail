using System;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.Models;
using APIResponse = Retail.Infrastructure.RequestModels.APIResponse;

namespace Retail.Infrastructure.ServiceLayer
{
    public class PriceDisplayTrackerManagement
    {
        public async Task<List<EntityKeyValueResponse>> GetCategoryForPriceDisplayTracker(long PersonId, long CountryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetCategoryForPriceDisplayTracker, PersonId, CountryId));
            return result;
        }

        public async Task<List<CatSubDisplayTrackerResponse>> GetCategoryForDisplayTracker(long PersonId,long CountryId,long StoreId)
        {
            var result = await WebServiceUtils<List<CatSubDisplayTrackerResponse>>.GetData(string.Format(ServiceEndPoints.GetCategoryForDisplayTracker, PersonId, CountryId, StoreId));
            return result;
        }

        public async Task<List<CatSubDisplayTrackerResponse>> GetSubCategoryForDisplayTracker(long PersonId,long CategoryId, long CountryId,long StoreId)
        {
            var result = await WebServiceUtils<List<CatSubDisplayTrackerResponse>>.GetData(string.Format(ServiceEndPoints.GetSubCategoryForDisplayTracker, PersonId, CategoryId, CountryId, StoreId));
            return result;
        }

        public async Task<List<ProductModelForPriceDisplayResponse>> GetProductModelForPriceDisplayTracker(long PersonId, long CountryId, long CategoryId, long SubCategoryId,long StoreId)
        {
            var result = await WebServiceUtils<List<ProductModelForPriceDisplayResponse>>.GetData(string.Format(ServiceEndPoints.GetProductModelForPriceDisplayTracker, PersonId, CountryId, CategoryId, SubCategoryId, StoreId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetSubCategoryForPriceDisplayTracker(long PersonId, long CategoryId, long CountryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetSubCategoryForPriceDisplayTracker, PersonId, CategoryId, CountryId));
            return result;
        }

        public async Task<PriceTrackerResponses> GetCompetitorProductModelByModelIdForPriceDisplayTracker(PriceTrackerListRequest priceTrackerListRequest)
        {
            var result = await WebServiceUtils<PriceTrackerResponses>.PostData(ServiceEndPoints.GetCompetitorProductModelByModelIdForPriceDisplayTracker, priceTrackerListRequest);
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetStoreForPriceDisplayTracker(long PersonId,long CountryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetStoreForPriceDisplayTracker, PersonId, CountryId));
            return result;
        }

        public async Task<APIResponse> PriceTrackerEntry(PriceTrackerEntryRequest priceTrackerEntryRequest)
        {
            return await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.PriceTrackerEntry, priceTrackerEntryRequest);
        }

        public async Task<List<ProductModelForPriceDisplayResponse>> SearchModelNumberByInitialForPriceDisplayTracker(long PersonId,long CountryId,long StoreId)
        {
            var result = await WebServiceUtils<List<ProductModelForPriceDisplayResponse>>.GetData(string.Format(ServiceEndPoints.SearchModelNumberByInitialForPriceDisplayTracker, PersonId, CountryId, StoreId));
            return result;
        }

        public async Task<ModelForDisTrackerResponse> GetProductModelForDisplayTracker(DisplayTrackerRequest displayTrackerRequest)
        {
            var result = await WebServiceUtils<ModelForDisTrackerResponse>.PostData(ServiceEndPoints.GetProductModelForDisplayTracker, displayTrackerRequest);
            return result;
        }

        public async Task<APIResponse> SaveDisplayTrackerEntry(DisplayTrackerEntryRequest displayTrackerEntryRequest)
        {
            return await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveDisplayTrackerEntry, displayTrackerEntryRequest);
        }

        public async Task<List<EntityKeyValueResponse>> GetStoreForDisplayTracker(long PersonId, long CountryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetStoreForDisplayTracker, PersonId, CountryId));
            return result;
        }
    }
}


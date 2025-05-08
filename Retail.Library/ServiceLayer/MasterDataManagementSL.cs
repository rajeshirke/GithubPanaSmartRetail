using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;

namespace Retail.Infrastructure.ServiceLayer
{
    public class MasterDataManagementSL
    {
        public async Task<List<EntityKeyValueResponse>> GetCitiesByCountryId(int CountryId)
        {
            return await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetCitiesByCountryId, CountryId));
        }

        //multiple countries
        public async Task<List<EntityKeyValueResponse>> GetMultipleCountryId()
        {
            return await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetMultipleCountryId));
        }

        //multiple Assigned Countries
        public async Task<List<EntityKeyValueResponse>> GetMultipleCountryIdByPersonId(long PersonId)
        {
            return await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetMultipleCountryIdByPersonId, PersonId));
        }

        //cities by multiple countries
        public async Task<List<EntityKeyValueResponse>> GetCitiesByMultipleCountryId(List<int> CountryIds)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(ServiceEndPoints.GetCitiesByMultipleCountryId,CountryIds);
            return result;
        }

        //accounts by multiple countries
        public async Task<List<EntityKeyValueResponse>> GetAccountsByMultipleCountryId(long? PersonId,List<int> CountryIds)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(string.Format(ServiceEndPoints.GetAccountsByMultipleCountryId, PersonId), CountryIds);
            return result;
        }

        //Locations by multiple country and multiple account
        public async Task<List<EntityKeyValueResponse>> GetLocationByAccountsMultipleCountryId(long? PersonId, ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(string.Format(ServiceEndPoints.GetLocationByAccountsMultipleCountryId, PersonId), reportCreateUpdateRequest);
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetLocationsByCountryId(int CountryId)
        {
            var result= await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationsByCountryId, CountryId));
            return result;
        }

        public async Task<List<LocationResponse>> GetLocationsWithDetailsCountryId(int CountryId)
        {
            return await WebServiceUtils<List<LocationResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationsWithDetailsCountryId, CountryId));
        }

        public async Task<List<LocationResponse>> GetLocationsWithDetailsPersonId(int PersonId)
        {
            return await WebServiceUtils<List<LocationResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationsWithDetailsPersonId, PersonId));
        }

        public async Task<List<EntityKeyValueResponse>> GetLocationsByCityIds(long? PersonID, List<int> CityIds)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(string.Format(ServiceEndPoints.GetLocationsByCityIds, PersonID), CityIds);
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetLocationsByPersonId(int PersonId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationsByPersonId, PersonId));
            return result;
        }

        //based on selected country and personID
        public async Task<List<EntityKeyValueResponse>> GetLocationsByPersonIdCountryIds(int PersonId,List<int?> SelectedCountryIds)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(string.Format(ServiceEndPoints.GetLocationsByPersonIdCountryIds, PersonId), SelectedCountryIds);
            return result;
        }

        public async Task<List<LocationResponse>> GetLocationDetailsByPersonId(int PersonId)
        {
            var result = await WebServiceUtils<List<LocationResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationDetailsByPersonId, PersonId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetPromotersByMultiLocation(List<int> LocationIds)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(ServiceEndPoints.GetPromotersByMultiLocation, LocationIds);
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetProductMasterCategories()
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetProductMasterCategories));
            return result;
        }

        public async Task<List<ProductCategoryResponse>> GetAllSubcategoriesforOffline() 
        {
            var result = await WebServiceUtils<List<ProductCategoryResponse>>.GetData(string.Format(ServiceEndPoints.GetAllSubcategoriesforOffline));
            return result;
        }

        public async Task<List<ProductModelWithCategoryResponse>> GetModelNumbersActiveByCountryIdOffline(long CountryId) 
        {
            var result = await WebServiceUtils<List<ProductModelWithCategoryResponse>>.GetData(string.Format(ServiceEndPoints.GetModelNumbersActiveByCountryIdOffline, CountryId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetProductSubCategoriesByCategoryId(long CategoryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetProductSubCategoriesByCategoryId, CategoryId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetAccountsByCountryId(int CountryId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetAccountsByCountryId, CountryId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetLocationsByAccountIDActive(long AccountId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationsByAccountIDActive, AccountId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetSupervisorsByAccountId(long AccountId)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.GetData(string.Format(ServiceEndPoints.GetSupervisorsByAccountId, AccountId));
            return result;
        }

        public async Task<List<EntityKeyValueResponse>> GetSupervisorsListByCountryAccountIdMultiple(ReportCreateUpdateRequest reportCreateUpdateRequest)
        {
            var result = await WebServiceUtils<List<EntityKeyValueResponse>>.PostData(ServiceEndPoints.GetSupervisorsListByCountryAccountIdMultiple, reportCreateUpdateRequest);
            return result;
        }

        public async Task<List<string>> SearchModelNumberByInitials(string ModelNumberinitials,long CountryId)
        {
            return await WebServiceUtils<List<string>>.GetData(string.Format(ServiceEndPoints.SearchModelNumberByInitials, ModelNumberinitials, CountryId));
        }
    }
}

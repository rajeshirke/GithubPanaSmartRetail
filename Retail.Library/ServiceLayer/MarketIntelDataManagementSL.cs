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
    public class MarketIntelDataManagementSL
    {
        //SaveSalesDataEntry
        public async Task<APIResponse> SaveMarketInsight(MarketInsightRequest marketInsightRequest)
        {
            var abc = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveMarketInsight, marketInsightRequest);
            return abc;
        }

        //Questionnaire, Product Test, Survey
        public async Task<APIResponse> CheckPersonHasAttendedAnyMarketIntelByMarketIntelId(int CountryId, int PersonId, int MarketIntelTypeId)
        {
            var result = await WebServiceUtils<APIResponse>.GetData(string.Format(ServiceEndPoints.CheckPersonHasAttendedAnyMarketIntelByMarketIntelId, CountryId, PersonId, MarketIntelTypeId));
            return result;
        }

        public async Task<List<MarketIntelQuestionAnswerToSubmitResponse>> GetMarketIntelCollectionQuestionAnswer(int MarketIntelId, int MarketIntelCollectionId)
        {
            var result = await WebServiceUtils<List<MarketIntelQuestionAnswerToSubmitResponse>>.GetData(string.Format(ServiceEndPoints.GetMarketIntelCollectionQuestionAnswer, MarketIntelId, MarketIntelCollectionId));
            return result;
        }

        public async Task<APIResponse> SaveMarketIntelAnswers(List<MarketIntelQuestionAnswerToSubmitResponse> MarketIntelAnswers)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveMarketIntelAnswers, MarketIntelAnswers);
            return result;
        }

        public async Task<APIResponse> CreateUpdateMarketIntelCollection(MarketIntelCollectionCreateUpdateRequest MarketIntelCollectionCreateUpdateRequests)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.CreateUpdateMarketIntelCollection, MarketIntelCollectionCreateUpdateRequests);
            return result;
        }

        public async Task<APIResponse> CheckQuestionnaireForAlert(int CountryId, int PersonId)
        {
            var result = await WebServiceUtils<APIResponse>.GetData(string.Format(ServiceEndPoints.CheckQuestionnaireForAlert, CountryId, PersonId));
            return result;
        }

        //List of active Market Intels
        public async Task<List<MarketIntel>> MarketIntelAllActiveDatas(int CountryId, int PersonId, int MarketIntelTypeId)
        {
            var result = await WebServiceUtils<List<MarketIntel>>.GetData(string.Format(ServiceEndPoints.MarketIntelAllActiveDatas, CountryId, PersonId, MarketIntelTypeId));
            return result;
        }
    }
}

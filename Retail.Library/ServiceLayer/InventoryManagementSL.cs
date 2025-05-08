using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using APIResponse = Retail.Infrastructure.RequestModels.APIResponse;
namespace Retail.Infrastructure.ServiceLayer
{
    public class InventoryManagementSL
    {
        public async Task<List<InventoryStockEntryResponse>> GetInventoryStockEntryByLocationPerson(int LocationId)
        {
            var result = await WebServiceUtils<List<InventoryStockEntryResponse>>.GetData(string.Format(ServiceEndPoints.GetInventoryStockEntryByLocationPerson, LocationId));
            return result;
        }

        public async Task<List<InventoryStockEntryRequest>> BuildStockEntryWithModelDetails(List<InventoryStockEntryRequest> inventoryStockEntryRequest)
        {
            var result = await WebServiceUtils<List<InventoryStockEntryRequest>>.PostData(ServiceEndPoints.BuildStockEntryWithModelDetails, inventoryStockEntryRequest);
            return result;
        }

        public async Task<APIResponse> CreateInventoryStockEntry(List<InventoryStockEntryRequest> inventoryStockEntryRequest)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.CreateInventoryStockEntry, inventoryStockEntryRequest);
            return result;
        }
    }
}

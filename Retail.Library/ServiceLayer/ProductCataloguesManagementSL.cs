using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.ResponseModels;

namespace Retail.Infrastructure.ServiceLayer
{
    public class ProductCataloguesManagementSL
    {
        public async Task<List<ProductCatalogueByCategoryResponse>> GetProductCataloguesByCountry(int CountryId)
        {
            return await WebServiceUtils<List<ProductCatalogueByCategoryResponse>>.GetData(string.Format(ServiceEndPoints.GetProductCataloguesByCountry, CountryId));
        }
    }
}

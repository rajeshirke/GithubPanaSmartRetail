using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Retail.Infrastructure.RequestModels;

namespace Retail.Infrastructure.ResponseModels
{
    public class ModelForDisTrackerResponse
    {
        public List<DisplayTrackerProductModelEntryRequest> ProductModels { get; set; }
        public List<BrandNameRequest> BrandNameRequest { get; set; }
        public List<DisplayTrackerListResponse> DisplayTrackerListResponse { get; set; }       
        public int OtherBrandQty { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Retail.Infrastructure.ResponseModels;

namespace Retail.Infrastructure.RequestModels
{
    public class DisplayTrackerEntryRequest
    {
        public DateTime Entrydate { get; set; }
        public int LocationId { get; set; }
        public String Comment { get; set; }
        public long PersonId { get; set; }
        //public int? OtherBrandQty { get; set; }
        public string OtherBrandQty { get; set; } 
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public List<DisplayTrackerProductModelEntryRequest> DisplaysTrackerProductModelEntryResponse { get; set; }
        public List<BrandNameRequest> DisplayTrackerCompetitorModelEntryRequests { get; set; }
        public virtual List<DisplayTrackerEntryImageRequest> DisplayTrackerEntryImageRequests { get; set; }
    }

    public class DisplayTrackerProductModelEntryRequest
    {
        public int DisplayTrackerId { get; set; }
        public long ProductModelId { get; set; }
        public String ProductModelName { get; set; }
        //public int? Qty { get; set; }
        public string Qty { get; set; }
    }

    public class BrandNameRequest
    {
        public string Comp_brand { get; set; }
        //public int? OtherQty { get; set; }
        public string OtherQty { get; set; }
        public int DisplayTrackerEntryBrandId { get; set; }
        public List<DisplayTrackerCompetitorModelEntryRequest> CompetitorModels { get; set; }

        [JsonIgnore]
        public string ArrowUrl { get; set; } = "nextnew.png";

        [JsonIgnore]
        public int RowHeight
        {
            get
            {
                if (CompetitorModels != null)
                {
                    return CompetitorModels.Count * 50;
                }

                return 20;
            }
        }
    }

    public class DisplayTrackerCompetitorModelEntryRequest
    {
        public int DisplayTrackerId { get; set; }
        public string BrandName { get; set; }
        public int CompetitorProductModelId { get; set; }
        public String ProductModelName { get; set; }
        //public int? Qty { get; set; }
        public string Qty { get; set; }
    }

    public class DisplayTrackerEntryImageRequest
    {
        public int DisplayTrackerEntryImageId { get; set; }
        public int DisplayTrackerId { get; set; }
        public long FileInfoId { get; set; }
        public virtual FileInfoResponse FileInfo { get; set; }
    }
}


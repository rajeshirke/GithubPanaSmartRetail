using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Retail.Infrastructure.ResponseModels
{
    public class ProductCatalogueResponse
    {
        public int ProductCatalogueId { get; set; }
        public int ProductCategoryId { get; set; }
        public long FileInfoId { get; set; }
        public string Title { get; set; }
        public int CountryId { get; set; }
        public long? UploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }


        public string ProductCategoryName { get; set; }
        public FileInfoResponse FileInfo { get; set; }
        // public virtual ProductCategory ProductCategory { get; set; }
        public string FileFullPath { get; set; }
        //public bool IsVisibleImage { get; set; }

        [JsonIgnore]
        public string FileFullPathMedia { get; set; }
    }


    public class ProductCatalogueByCategoryResponse
    {
        
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public List<ProductCatalogueResponse> ProductCatalogues { get; set; }
    }


    //public class ProductCatalogueByCountryResponse
    //{ 
    //    public int ProductCountryId { get; set; }
    //    public string ProductCountryName { get; set; }

    //    public List<ProductCatalogueByCategoryResponse> ProductCatalogueByCategoryResponses { get; set; }
    //}

}

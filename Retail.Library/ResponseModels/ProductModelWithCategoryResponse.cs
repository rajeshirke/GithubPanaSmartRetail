using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class ProductModelWithCategoryResponse
    {
        public long ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public int CountryId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}

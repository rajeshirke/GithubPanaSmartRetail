using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class ProductCategoryResponse
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
    }
}

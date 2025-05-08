using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class LocationResponse
    {
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationStoreName { get; set; }
        public string Area { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string City { get; set; }
        public double Distance { get; set; }
        public int LocationAccountID { get; set; }

    }
}

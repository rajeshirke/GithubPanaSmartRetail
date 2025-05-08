using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.RequestModels
{
    public class TargetsOverviewByCityStoreRequest
    { 
        public List<int> CityIds { get; set; }
        public List<string> CityNames { get; set; }
        public List<int> LocationIds { get; set; }
        public List<string> LocationNames { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

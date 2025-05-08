using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class MobileMainMenuResponse
    {
        //public int MobileMainMenuId { get; set; }
        //public string Name { get; set; }
        //public int? MenuOrder { get; set; }
        //public string MenuMappingName { get; set; }

        //public int ApplicationModuleId { get; set; }
        //public string ModuleName { get; set; }

        public int MobileMainMenuId { get; set; }
        public string Name { get; set; }
        public int? MenuOrder { get; set; }
        public string MenuMappingName { get; set; }

        public int ApplicationModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}

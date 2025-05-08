using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class RoleMandatoryDetails
    {
        public bool IsStoreSelectionMandatory { get; set; }
        public bool IsShiftMandatory { get; set; }
        public bool IsCompanyMandatory { get; set; }
    }
}

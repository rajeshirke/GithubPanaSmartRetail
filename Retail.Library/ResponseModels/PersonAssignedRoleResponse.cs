using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class PersonAssignedRoleResponse
    {
        public int PersonAssignedRoleId { get; set; }
        public long PersonId { get; set; }
        public int PersonRoleId { get; set; }          
        public string PersonRoleName  { get; set; }
    }
}

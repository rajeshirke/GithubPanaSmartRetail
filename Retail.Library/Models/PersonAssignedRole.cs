using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonAssignedRole
    {
        public int PersonAssignedRoleId { get; set; }
        public long PersonId { get; set; }
        public int PersonRoleId { get; set; }

        public virtual Person Person { get; set; }
        public virtual PersonRole PersonRole { get; set; }
    }
}

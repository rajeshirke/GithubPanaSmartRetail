using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonRoleAppAccess
    {
        public int PersonRoleAppAccessId { get; set; }
        public int PersonRoleId { get; set; }
        public int AppAccessTypeId { get; set; }

        public virtual PersonRole PersonRole { get; set; }
    }
}

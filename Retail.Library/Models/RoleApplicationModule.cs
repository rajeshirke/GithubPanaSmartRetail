using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class RoleApplicationModule
    {
        public int Id { get; set; }
        public int ApplicationModuleId { get; set; }
        public int PersonRoleId { get; set; }

        public virtual ApplicationModule ApplicationModule { get; set; }
        public virtual PersonRole PersonRole { get; set; }
    }
}

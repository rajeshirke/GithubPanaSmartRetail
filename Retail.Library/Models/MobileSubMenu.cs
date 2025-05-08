using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MobileSubMenu
    {
        public int MobileSubMenuId { get; set; }
        public string Name { get; set; }
        public int MobileMainMenuId { get; set; }
        public int? PersonRoleId { get; set; }

        public virtual MobileMainMenu MobileMainMenu { get; set; }

    }
}

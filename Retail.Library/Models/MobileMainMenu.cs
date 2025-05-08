using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MobileMainMenu
    {
        public MobileMainMenu()
        {
            MobileSubMenus = new HashSet<MobileSubMenu>();
        }

        public int MobileMainMenuId { get; set; }
        public string Name { get; set; }
        public int? MenuOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? ModuleId { get; set; }

        public virtual ApplicationModule Module { get; set; }
        public virtual ICollection<MobileSubMenu> MobileSubMenus { get; set; }
    }
}

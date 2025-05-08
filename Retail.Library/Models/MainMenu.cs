using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class MainMenu
    {
        //public MainMenu()
        //{
        //    SubMenus = new HashSet<SubMenu>();
        //}

        //public int MainMenuId { get; set; }
        //public string MainMenuName { get; set; }
        //public int? MenuOrder { get; set; }
        //public bool? IsActive { get; set; }
        //public string Controller { get; set; }
        //public string Action { get; set; }
        //public int? ModuleId { get; set; }
        //public bool? IsMasterManageMenu { get; set; }

        //public virtual ApplicationModule Module { get; set; }
        //public virtual ICollection<SubMenu> SubMenus { get; set; }

        public MainMenu()
        {
            SubMenus = new HashSet<SubMenu>();
        }

        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public int? MenuOrder { get; set; }
        public bool? IsActive { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int? ModuleId { get; set; }
        public bool? IsMasterManageMenu { get; set; }

        public virtual ApplicationModule Module { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}

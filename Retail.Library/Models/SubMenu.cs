using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class SubMenu
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int? MainMenuId { get; set; }
        public int? SubMenuOrder { get; set; }
        public int? PersonRoleId { get; set; }

        public virtual MainMenu MainMenu { get; set; }
    }
}

using System;
namespace Retail.Infrastructure.Models
{
    public class RoleMobileSubMenu
    {
        public int RoleMobileSubMenuId { get; set; }
        public int MobileSubMenuId { get; set; }
        public int PersonRoleId { get; set; }
    }
}

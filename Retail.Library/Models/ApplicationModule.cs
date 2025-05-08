using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class ApplicationModule
    {
        public ApplicationModule()
        {
            ApplicationModuleActions = new HashSet<ApplicationModuleAction>();
            CountryApplicationModules = new HashSet<CountryApplicationModule>();
            MainMenus = new HashSet<MainMenu>();
            MobileMainMenus = new HashSet<MobileMainMenu>();
            RoleApplicationModules = new HashSet<RoleApplicationModule>();
        }

        public int ApplicationModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsInMobileApp { get; set; }

        public virtual ICollection<ApplicationModuleAction> ApplicationModuleActions { get; set; }
        public virtual ICollection<CountryApplicationModule> CountryApplicationModules { get; set; }
        public virtual ICollection<MainMenu> MainMenus { get; set; }
        public virtual ICollection<MobileMainMenu> MobileMainMenus { get; set; }
        public virtual ICollection<RoleApplicationModule> RoleApplicationModules { get; set; }
    }
}

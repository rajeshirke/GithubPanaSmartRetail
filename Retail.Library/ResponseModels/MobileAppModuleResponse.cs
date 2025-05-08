using System;
namespace Retail.Infrastructure.ResponseModels
{
    public  class MobileAppModuleResponse
    {
        //public MobileAppModuleResponse()
        //{
        //    CountryMobileModules = new HashSet<CountryMobileModule>();
        //}

        public int MobileAppModuleId { get; set; }
        public string Name { get; set; }
        public int PersonRoleId { get; set; }

        //public virtual ICollection<MobileMainMenu> MobileMainMenus { get; set; }


        // public  ICollection<CountryMobileModuleResponse> CountryMobileModules { get; set; }
    }
}

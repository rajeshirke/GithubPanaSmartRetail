using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class MobileSubMenuResponse
    {
        public int MobileSubMenuId { get; set; }
        public string Name { get; set; }
        public int MobileMainMenuId { get; set; }
    }
}

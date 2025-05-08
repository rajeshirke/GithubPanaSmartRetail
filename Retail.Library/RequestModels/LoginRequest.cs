using System;
namespace Retail.Infrastructure.RequestModels
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}

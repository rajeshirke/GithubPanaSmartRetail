using System;
namespace Retail.Infrastructure.RequestModels
{
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //Device Information
        public string DeviceID { get; set; }
        public string DeviceModel { get; set; }
        public string DevicePlatform { get; set; }
        public string OSVersion { get; set; }
        public int AppVersion { get; set; }
    }
}

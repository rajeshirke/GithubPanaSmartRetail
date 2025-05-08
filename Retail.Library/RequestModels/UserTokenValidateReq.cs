using System;
namespace Retail.Infrastructure.RequestModels
{
    public class UserTokenValidateReq
    {
        public int tokenId { get; set; }
        public string tokenValue { get; set; }
        public int tokenContextId { get; set; }
        public string userId { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string value { get; set; }
    }
}

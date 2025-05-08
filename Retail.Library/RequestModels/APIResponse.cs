using System;
using Retail.Infrastructure.Models;

namespace Retail.Infrastructure.RequestModels
{
    public class APIResponse
    {
        public APIResponse()
        {

        }

        public Object Data { get; set; }
        //public MarketIntel Data { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public APIResponse(Object data, int status, string message, string errorMessage = "")
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
            this.ErrorMessage = errorMessage;

        }
    }
}

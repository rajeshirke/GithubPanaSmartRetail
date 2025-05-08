using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class APIResponse
    {
        public dynamic Data { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public APIResponse(dynamic data, int status, string message, string errorMessage = "")
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
            this.ErrorMessage = errorMessage;

        } 
    }
}

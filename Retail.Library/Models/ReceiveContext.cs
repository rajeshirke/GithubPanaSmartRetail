using System;
namespace Retail.Infrastructure.Models
{
    public class ReceiveContext<T>
    {

        public int status { get; set; }
        public string errorMessage { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
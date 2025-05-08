using System;
namespace Retail.Infrastructure.ResponseModels
{
    public class PersonServiceCenterResponse
    {
        public int PersonServiceCenterId { get; set; }
        public long PersonId { get; set; }
        public int ServiceCenterId { get; set; }

        public ServiceCenterResponse ServiceCenter { get; set; }
    }
}

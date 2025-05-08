using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Retail.Infrastructure.ResponseModels
{
    public class PersonLoginResponse : PersonDetailsResponse
    {
        public CountryResponse CountryResponse { get; set; }

        public virtual ICollection<PersonServiceCenterResponse> PersonServiceCenters { get; set; }
    }
}

using System;
namespace Retail.Infrastructure.RequestModels
{
    public class PersonUpdateRequest
    {
        public long PersonId { get; set; }
        public int? CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PreferredLanguageId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }

        public int? Salutation { get; set; }
    }
}

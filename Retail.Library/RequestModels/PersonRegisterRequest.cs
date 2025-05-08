using System;
namespace Retail.Infrastructure.RequestModels
{
    public class PersonRegisterRequest
    {
        public long PersonId { get; set; }
        public Guid UserId { get; set; }
        public long? ParentPersonId { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PreferredLanguageId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? Salutation { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PersonPassword { get; set; }
        public int? PersonRoleId { get; set; }
        public int? PersonStatusId { get; set; }
        public bool? IsEmailValidated { get; set; }
        public bool? IsMobileValidated { get; set; }
        public bool? IsPasswordChanged { get; set; }

    }
}

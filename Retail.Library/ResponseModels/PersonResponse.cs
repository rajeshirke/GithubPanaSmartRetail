using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class PersonResponse
    {
        public long PersonId { get; set; }
        public Guid UserId { get; set; }
        public long? ParentPersonId { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        public int? Salutation { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PersonStatusId { get; set; }
        public bool? IsEmailValidated { get; set; }
        public bool? IsMobileValidated { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public long? ProfilePictureFileInfoId { get; set; }
        public bool? IsActive { get; set; }
        public string PersonProfileStatus { get; set; }
        public int PersonRoleId { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }

}

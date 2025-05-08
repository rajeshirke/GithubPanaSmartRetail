using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class PersonsVMResponse
    {
        public long PersonID { get; set; }
        public Guid UserID { get; set; }
        public long? ParentPersonID { get; set; }
        public int CountryID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PreferredLanguageID { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? Salutation { get; set; }
        public string CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PersonRoleID { get; set; }
        public int? PersonStatusID { get; set; }
        public bool? IsEmailValidated { get; set; }
        public bool? IsMobileValidated { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public string PersonStatusName { get; set; }
        public string PersonRoleName { get; set; }
    }

}

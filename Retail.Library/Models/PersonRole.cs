using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonRole
    {
        public PersonRole()
        {
            PersonAssignedRoles = new HashSet<PersonAssignedRole>();
            PersonRoleAppAccesses = new HashSet<PersonRoleAppAccess>();
            RoleApplicationModules = new HashSet<RoleApplicationModule>();
        }

        public int PersonRoleId { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsStoreMandatory { get; set; }
        public bool? IsShiftMandatory { get; set; }
        public bool? IsCompanyMandatory { get; set; }

        public virtual ICollection<PersonAssignedRole> PersonAssignedRoles { get; set; }
        public virtual ICollection<PersonRoleAppAccess> PersonRoleAppAccesses { get; set; }
        public virtual ICollection<RoleApplicationModule> RoleApplicationModules { get; set; }
    }
}

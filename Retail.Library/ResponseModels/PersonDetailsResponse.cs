using Retail.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infrastructure.ResponseModels
{
    public class PersonDetailsResponse
    {
        //public long PersonId { get; set; }
        //public Guid UserId { get; set; }
        //public long? ParentPersonId { get; set; }
        //public int CountryId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string MobileNumber { get; set; }
        //public string AlternateMobileNumber { get; set; }
        //public int? Salutation { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public int? PersonStatusId { get; set; }
        //public bool? IsEmailValidated { get; set; }
        //public bool? IsMobileValidated { get; set; }
        //public bool? IsPasswordChanged { get; set; }
        //public long? ProfilePictureFileInfoId { get; set; }
        //public bool? IsActive { get; set; }
        //public string PersonProfileStatus { get; set; }
        //public int PersonRoleId { get; set; }
        //public string CurrencyCode { get; set; }
        //public string FullName
        //{
        //    get { return FirstName + " " + LastName; }
        //}
        //public string CountryName { get; set; }
        ////public ICollection<PersonRoleResponse> PersonRoles { get; set; }
        ////public   ICollection<PersonCompany> PersonCompanies { get; set; }
        //public ICollection<CountryResponse> Countries { get; set; }
        //public ICollection<LocationResponse> Locations { get; set; }

        //public Salutation SalutationNavigation { get; set; } 

        //public virtual FileInfoResponse ProfilePictureFileInfo { get; set; }

        //public virtual ICollection<MainMenu> MainMenus { get; set; }
        //public virtual ICollection<MobileMainMenuResponse> MobileMainMenus { get; set; }

        //public virtual List<MobileAppModuleResponse> MobileAppModules { get; set; }//prj
        ////public bool? IsEmailValidated { get; set; }
        ////public bool? IsMobileValidated { get; set; }
        ////public bool? IsPasswordChanged { get; set; }


        public long PersonId { get; set; }
        public Guid UserId { get; set; }
        public long? ParentPersonId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }//--added

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

        public string CurrencyCode { get; set; }

        public int? DutyHours { get; set; }//new 
        //public ICollection<PersonRoleResponse> PersonRoles { get; set; }
        //public ICollection<PersonAssignedRoleResponse> PersonAssignedRoles { get; set; }

        //public   ICollection<PersonCompany> PersonCompanies { get; set; }
        public ICollection<CountryResponse> Countries { get; set; }
        public ICollection<LocationResponse> Locations { get; set; }

        public Salutation SalutationNavigation { get; set; }

        public virtual FileInfoResponse ProfilePictureFileInfo { get; set; }

        public virtual ICollection<MainMenu> MainMenus { get; set; }
        public virtual ICollection<MobileMainMenuResponse> MobileMainMenus { get; set; }

        //public bool? IsEmailValidated { get; set; }
        //public bool? IsMobileValidated { get; set; }
        //public bool? IsPasswordChanged { get; set; }

        public ICollection<PersonAssignedRoleResponse> PersonAssignedRoles { get; set; } //new added
        public virtual ICollection<MobileSubMenuResponse> MobileSubMenus { get; set; }
        //public virtual ICollection<ApplicationModuleAction> ApplicationModuleActions { get; set; } ////for inventory option show hide

        public virtual ICollection<ShiftResponse> Shifts { get; set; }
    }
}

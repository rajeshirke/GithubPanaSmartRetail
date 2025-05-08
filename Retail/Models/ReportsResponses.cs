using System;
namespace Retail.Models
{
    public class ReportsResponses
    {

        public class SelectedItems
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class AccountListBasedonSelectedCity
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class SelectedCountries
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class SelectedSupervisors
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class Cities
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class AssignedLocations
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class SelectedSubcategories
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}

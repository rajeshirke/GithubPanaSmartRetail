using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonStatu
    {
        public PersonStatu()
        {
            Persons = new HashSet<Person>();
        }

        public int PersonStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}

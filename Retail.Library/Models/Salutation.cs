using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Salutation
    {
        public Salutation()
        {
            Persons = new HashSet<Person>();
        }

        public int SalutationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}

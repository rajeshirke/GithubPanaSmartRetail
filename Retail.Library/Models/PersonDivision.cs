using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class PersonDivision
    {
        public long PersonDeivisionId { get; set; }
        public long PersonId { get; set; }
        public int DivisionId { get; set; }

        public virtual Division Division { get; set; }
    }
}

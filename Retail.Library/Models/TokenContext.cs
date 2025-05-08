using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class TokenContext
    {
        public TokenContext()
        {
            Tokens = new HashSet<Token>();
        }

        public int TokenContextId { get; set; }
        public string Name { get; set; }
        public int DurationInSeconds { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}

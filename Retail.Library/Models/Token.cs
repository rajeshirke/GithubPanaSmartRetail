using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public int TokenContextId { get; set; }
        public Guid UserId { get; set; }
        public string Value { get; set; }
        public DateTime ExpireTime { get; set; }
        public int FailedAttempts { get; set; }

        public virtual TokenContext TokenContext { get; set; }
    }
}

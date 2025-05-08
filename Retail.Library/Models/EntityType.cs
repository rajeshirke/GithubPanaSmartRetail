using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            Entities = new HashSet<Entity>();
        }

        public int EntityTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
    }
}

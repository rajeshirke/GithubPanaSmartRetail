using System;
using System.Collections.Generic;

#nullable disable

namespace Retail.Infrastructure.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int EntityId { get; set; }
        public int? EntityIdInDb { get; set; }
        public int EntityTypeId { get; set; }

        public virtual EntityType EntityType { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}

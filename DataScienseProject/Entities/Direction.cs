using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Direction
    {
        public Direction()
        {
            Tags = new HashSet<Tag>();
        }

        public int DirectionKey { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}

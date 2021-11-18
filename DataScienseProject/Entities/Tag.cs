using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject
{
    public partial class Tag
    {
        public Tag()
        {
            ViewTags = new HashSet<ViewTag>();
        }

        public int TagKey { get; set; }
        public int? DirectionKey { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public virtual Direction DirectionKeyNavigation { get; set; }
        public virtual ICollection<ViewTag> ViewTags { get; set; }
    }
}

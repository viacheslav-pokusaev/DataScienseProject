using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class LinkType
    {
        public LinkType()
        {
            Elements = new HashSet<Element>();
        }

        public int LinkTypeKey { get; set; }
        public string LinkTypeName { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject
{
    public partial class ElementType
    {
        public ElementType()
        {
            Elements = new HashSet<Element>();
        }

        public int ElementTypeKey { get; set; }
        public string ElementTypeName { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}

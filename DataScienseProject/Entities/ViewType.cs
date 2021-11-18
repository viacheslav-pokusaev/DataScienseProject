using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ViewType
    {
        public ViewType()
        {
            Views = new HashSet<View>();
        }

        public int ViewTypeKey { get; set; }
        public string ViewTypeName { get; set; }

        public virtual ICollection<View> Views { get; set; }
    }
}

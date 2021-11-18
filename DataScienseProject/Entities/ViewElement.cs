using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ViewElement
    {
        public long ViewElementKey { get; set; }
        public long? ViewKey { get; set; }
        public long? ElementKey { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Element ElementKeyNavigation { get; set; }
        public virtual View ViewKeyNavigation { get; set; }
    }
}

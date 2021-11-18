using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class VisitView
    {
        public long VisitViewKey { get; set; }
        public long? VisitKey { get; set; }
        public long? ViewKey { get; set; }
        public DateTime? VisitDate { get; set; }

        public virtual View ViewKeyNavigation { get; set; }
        public virtual VisitLog VisitKeyNavigation { get; set; }
    }
}

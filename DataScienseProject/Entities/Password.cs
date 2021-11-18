using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject
{
    public partial class Password
    {
        public Password()
        {
            VisitLogs = new HashSet<VisitLog>();
        }

        public long PasswordKey { get; set; }
        public int? GroupKey { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Group GroupKeyNavigation { get; set; }
        public virtual ICollection<VisitLog> VisitLogs { get; set; }
    }
}

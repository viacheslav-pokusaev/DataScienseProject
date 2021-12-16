using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Password
    {
        public Password()
        {
            VisitLogs = new HashSet<VisitLog>();
        }

        public long PasswordKey { get; set; }
        public int? GroupKey { get; set; }
        public string PasswordValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Group GroupKeyNavigation { get; set; }
        public virtual ICollection<VisitLog> VisitLogs { get; set; }
    }
}

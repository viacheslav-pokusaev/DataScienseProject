using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class VisitLog
    {
        public VisitLog()
        {
            Feedbacks = new HashSet<Feedback>();
            VisitViews = new HashSet<VisitView>();
        }

        public long VisitKey { get; set; }
        public long? PasswordKey { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool? IsVisitSuccess { get; set; }
        public DateTime? VisitLastClickDate { get; set; }
        public string IpAddress { get; set; }

        public virtual Password PasswordKeyNavigation { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<VisitView> VisitViews { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class View
    {
        public View()
        {
            GroupViews = new HashSet<GroupView>();
            ViewElements = new HashSet<ViewElement>();
            ViewExecutors = new HashSet<ViewExecutor>();
            ViewTags = new HashSet<ViewTag>();
            VisitViews = new HashSet<VisitView>();
        }

        public long ViewKey { get; set; }
        public int? ViewTypeKey { get; set; }
        public string ViewName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LogoPath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ViewType ViewTypeKeyNavigation { get; set; }
        public virtual ICollection<GroupView> GroupViews { get; set; }
        public virtual ICollection<ViewElement> ViewElements { get; set; }
        public virtual ICollection<ViewExecutor> ViewExecutors { get; set; }
        public virtual ICollection<ViewTag> ViewTags { get; set; }
        public virtual ICollection<VisitView> VisitViews { get; set; }
    }
}

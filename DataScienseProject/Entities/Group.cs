using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Group
    {
        public Group()
        {
            GroupViews = new HashSet<GroupView>();
            Passwords = new HashSet<Password>();
        }

        public int GroupKey { get; set; }
        public string GroupName { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<GroupView> GroupViews { get; set; }
        public virtual ICollection<Password> Passwords { get; set; }
    }
}

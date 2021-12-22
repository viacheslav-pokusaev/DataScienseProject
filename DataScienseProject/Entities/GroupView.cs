#nullable disable

namespace DataScienseProject.Entities
{
    public partial class GroupView
    {
        public long GroupViewKey { get; set; }
        public long? ViewKey { get; set; }
        public int? GroupKey { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Group GroupKeyNavigation { get; set; }
        public virtual View ViewKeyNavigation { get; set; }
    }
}

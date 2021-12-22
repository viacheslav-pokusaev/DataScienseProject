#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ViewTag
    {
        public long ViewTagKey { get; set; }
        public long? ViewKey { get; set; }
        public int? TagKey { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Tag TagKeyNavigation { get; set; }
        public virtual View ViewKeyNavigation { get; set; }
    }
}

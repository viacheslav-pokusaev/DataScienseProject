#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ElementParameter
    {
        public long ElementParameterKey { get; set; }
        public long? ElementKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Element ElementKeyNavigation { get; set; }
    }
}

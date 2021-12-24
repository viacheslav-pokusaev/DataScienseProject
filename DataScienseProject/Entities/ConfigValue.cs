#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ConfigValue
    {
        public long ConfigValuesKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsEnabled { get; set; }
    }
}

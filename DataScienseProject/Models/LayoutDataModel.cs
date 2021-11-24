using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models
{
    public class LayoutDataModel
    {
        public string ElementName { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public string ValueText { get; set; }
        public string ElementTypeName { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsShowElementName { get; set; }
        public List<LayoutStyleModel> LayoutStyleModel { get; set; }
    }
}

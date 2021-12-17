using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models
{
    public class TechnologyModel
    {
        public string TagName { get; set; }
        public string TagLink { get; set; }
        public string DirectoryName { get; set; }
        public string DirectoryLink { get; set; }
        public int? OrderNumber { get; set; }
    }
}

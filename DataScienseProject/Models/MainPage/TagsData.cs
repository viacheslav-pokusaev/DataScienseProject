using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models.MainPage
{
    public class TagsData : TagModel
    {
        public int ViewKey { get; set; }
        public string ViewName { get; set; }
        public int? OrderNumber { get; set; }
    }
}

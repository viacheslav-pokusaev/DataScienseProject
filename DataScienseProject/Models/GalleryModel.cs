using DataScienseProject.Entities;
using DataScienseProject.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models
{
    public class GalleryModel
    {
        public long ViewKey { get; set; }
        public string ViewName { get; set; }
        public string Image { get; set; }
        public List<ExecutorModel> Executors { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ShortDescription { get; set; }
        public int OrderNumber { get; set; }
    }
}

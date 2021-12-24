
using System.Collections.Generic;

namespace DataScienseProject.Models
{
    public class GalleryModel
    {
        public long ViewKey { get; set; }
        public string ViewName { get; set; }
        public string Image { get; set; }
        public List<string> Executors { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ShortDescription { get; set; }
        public int OrderNumber { get; set; }
    }
}

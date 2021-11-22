using DataScienseProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models
{
    public class MainPageModel
    {
        //public View Views { get; set; }
        //public ViewExecutor ViewExecutors { get; set; }
        //public ViewTag ViewTags { get; set; }
        //public Element Elements { get; set; }

        public ProjectTypeModel ProjectTypeModel { get; set; }
        public ExecutorModel ExecutorModel { get; set; }
        public List<TehnologyModel> TehnologyModels { get; set; }
        public List<LayoutDataModel> LayoutDataModels { get; set; }
        public List<LatoutStyleModel> LatoutStyleModels { get; set; }
    }
}

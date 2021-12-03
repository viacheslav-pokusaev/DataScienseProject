using DataScienseProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models
{
    public class MainPageModel
    {
        public List<ProjectTypeModel> ProjectTypeModels { get; set; }
        public List<ExecutorModel> ExecutorModels { get; set; }
        public List<TechnologyModel> TehnologyModels { get; set; }
        public List<LayoutDataModel> LayoutDataModels { get; set; }
    }
}

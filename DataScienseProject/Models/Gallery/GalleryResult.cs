using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models.Gallery
{
    public class GalleryResult
    {
        public List<GalleryModel> GalleryModels { get; set; }
        public StatusModel ExceptionModel { get; set; }
    }
}

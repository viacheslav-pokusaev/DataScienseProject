using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IGetDataService
    {
        MainPageModel GetMainPageData(int id);
        GalleryResult GetGalleryPageData(string groupName, HttpContext cookies);
    }
}

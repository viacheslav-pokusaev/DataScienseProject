using DataScienseProject.Models;
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
        List<GalleryModel> GetGalleryPageData(string groupName, HttpContext http);
    }
}

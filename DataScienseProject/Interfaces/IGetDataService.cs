using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DataScienseProject.Interfaces
{
    public interface IGetDataService
    {
        MainPageModel GetMainPageData(int id);
        GalleryResult GetGalleryPageData(string groupName, HttpContext http, FilterModel filter);
        bool UniqualityCheck(GalleryModel galleryModel, List<GalleryModel> currentList);
    }
}

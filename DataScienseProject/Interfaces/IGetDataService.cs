﻿using DataScienseProject.Models;
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
        MainPageModel GetMainPageData();
        GalleryResult GetGalleryPageData(string groupName, HttpContext http, FilterModel filter);
        bool UniqulityCheck(GalleryModel galleryModel, List<GalleryModel> currentList);
    }
}

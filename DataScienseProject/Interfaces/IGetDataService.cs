using DataScienseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IGetDataService
    {
        MainPageModel GetMainPageData();
        List<GaleryModel> GetGaleryPageData(string groupName);
    }
}

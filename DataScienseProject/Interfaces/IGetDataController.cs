using DataScienseProject.Models;

namespace DataScienseProject.Interfaces
{
    public interface IGetDataController
    {
        MainPageModel GetMainPageData();
        public MainPageModel GetGaleryData();
    }
}

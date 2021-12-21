using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Mvc;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly IGetDataService _getDataService;
        public GetDataController(IGetDataService getDataService)
        {
            _getDataService = getDataService;
        }

        [HttpGet]
        [Route("gallery/model/{id}")]
        public MainPageModel GetMainPageData(int id)
        {
            return _getDataService.GetMainPageData(id);
        }
        [HttpGet]
        [Route("gallery/{groupName}")]
        public GalleryResult GetGaleryData(string groupName)
        {
            return _getDataService.GetGalleryPageData(groupName, HttpContext, null);
        }

        [HttpPost]
        [Route("gallery")]
        public GalleryResult GetGaleryData([FromBody] FilterModel filter)
        {
            return _getDataService.GetGalleryPageData(filter.GroupName, HttpContext, filter);
        }
    }
}

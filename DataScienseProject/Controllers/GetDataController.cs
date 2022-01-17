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
        private readonly IAuthorizationService _authorizationService;
        public GetDataController(IGetDataService getDataService, IAuthorizationService authorizationService)
        {
            _getDataService = getDataService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("gallery/model/{id}")]
        public MainPageModel GetMainPageData(int id)
        {
            _authorizationService.UpdateCookie(HttpContext);
            return _getDataService.GetMainPageData(id);
        }
        [HttpGet]
        [Route("gallery/{groupName}")]
        public GalleryResult GetGaleryData(string groupName)
        {
            _authorizationService.UpdateCookie(HttpContext);
            return _getDataService.GetGalleryPageData(groupName, HttpContext, null);
        }

        [HttpPost]
        [Route("gallery")]
        public GalleryResult GetGaleryData([FromBody] FilterModel filter)
        {
            _authorizationService.UpdateCookie(HttpContext);
            return _getDataService.GetGalleryPageData(filter.GroupName, HttpContext, filter);
        }
    }
}

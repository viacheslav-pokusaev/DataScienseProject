using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetDataController :  ControllerBase
    {
        private readonly IGetDataService _getDataService;
        public GetDataController(IGetDataService getDataService)
        {
            _getDataService = getDataService;
        }

        [HttpGet]
        [Route("main")]
        public MainPageModel GetMainPageData()
        {
            return _getDataService.GetMainPageData();            
        }
        [HttpGet]
        [Route("gallery/{groupName}")]
        public GalleryResult GetGaleryData(string groupName)
        {
            return _getDataService.GetGalleryPageData(groupName, HttpContext, null);
        }

        [HttpPost]
        [Route("gallery")]
        public GalleryResult GetGaleryData([FromBody]FilterModel filter)
        {
            return _getDataService.GetGalleryPageData(filter.GroupName, HttpContext, filter);
        }
    }
}

using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
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
    public class GetDataController :  ControllerBase, IGetDataController
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
            var test = _getDataService.GetMainPageData();
            return new MainPageModel();
        }
        [HttpGet]
        [Route("galery")]
        public MainPageModel GetGaleryData()
        {
            var test = _getDataService.GetGaleryPageData();
            throw new NotImplementedException();
        }
    }
}

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
        private readonly CS_DS_PortfolioContext _context;
        private readonly IGetDataService _getDataService;
        public GetDataController(CS_DS_PortfolioContext context, IGetDataService getDataService)
        {
            _context = context;
            _getDataService = getDataService;
        }

        [HttpGet]
        [Route("main")]
        public MainPageModel GetMainPageData()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("galery")]
        public MainPageModel GetGaleryData()
        {
            throw new NotImplementedException();
        }
    }
}

using DataScienseProject.Interfaces;
using DataScienseProject.Models.MainPage;
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
    public class MainPageController : ControllerBase
    {
        private readonly IMainPageService _mainPageService;
        
        public MainPageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;           
        }

        [HttpPost]
        [Route("add")]
        public void Add([FromBody] List<TagModel> tags)
        {
            _mainPageService.AddNewGroup(tags);
        }

        [HttpGet]
        [Route("tags")]
        public List<TagModel> GetAllTags()
        {
            return _mainPageService.GetAllTags();
        }

    }
}

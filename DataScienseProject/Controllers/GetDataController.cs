﻿using DataScienseProject.Context;
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
        public List<GalleryModel> GetGalleryData(string groupName)
        {
            return _getDataService.GetGalleryPageData(groupName);
        }

        [HttpPost]
        [Route("add-feedback")]
        public bool AddFeedback([FromBody] FeedbackModel feedback)
        {
            return _getDataService.AddFeedback(feedback);
        }
    }
}

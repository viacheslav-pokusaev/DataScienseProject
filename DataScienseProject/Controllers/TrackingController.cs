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
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _logsService;
        public TrackingController(ITrackingService logsService)
        {
            _logsService = logsService;
        }
        public void GetLogs(TrackingModel tracking)
        {
            _logsService.GetLogs(tracking);
        }
    }
}

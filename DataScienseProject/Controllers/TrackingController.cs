using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        public TrackingController(ITrackingService logsService)
        {
            _trackingService = logsService;
        }
        [HttpPost]
        [Route("tracking-data")]
        public void GetTrackingData(TrackingModel tracking)
        {
            _trackingService.SaveTrackingData(tracking, HttpContext);
        }
    }
}

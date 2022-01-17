using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DataScienseProject.Services
{
    public class TrackingService: ITrackingService
    {
        private readonly IEncriptionService _encriptionService;
        private readonly DataScienceProjectDbContext _context;
        public TrackingService(DataScienceProjectDbContext context, IEncriptionService encriptionService) {
            _context = context;
            _encriptionService = encriptionService;
        }

        public void GetTrackingData(TrackingModel tracking, HttpContext http)
        {
            tracking.Password = _encriptionService.DescriptPassword(http.Request.Cookies.FirstOrDefault(x => x.Key == "Password").Value);
        }
    }
}

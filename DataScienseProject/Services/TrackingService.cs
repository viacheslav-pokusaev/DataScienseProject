using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;

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

        public void GetTrackingData(TrackingModel tracking)
        {
            
        }
    }
}

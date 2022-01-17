
using DataScienseProject.Models;
using Microsoft.AspNetCore.Http;

namespace DataScienseProject.Interfaces
{
    public interface ITrackingService
    {
        void SaveTrackingData(TrackingModel tracking, HttpContext http);
    }
}

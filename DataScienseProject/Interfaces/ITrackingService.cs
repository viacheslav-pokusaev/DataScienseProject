
using DataScienseProject.Models;
using Microsoft.AspNetCore.Http;

namespace DataScienseProject.Interfaces
{
    public interface ITrackingService
    {
        void GetTrackingData(TrackingModel tracking, HttpContext http);
    }
}

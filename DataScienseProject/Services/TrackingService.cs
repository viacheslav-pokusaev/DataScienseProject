using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DataScienseProject.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IEncriptionService _encriptionService;
        private readonly DataScienceProjectDbContext _context;
        public TrackingService(DataScienceProjectDbContext context, IEncriptionService encriptionService)
        {
            _context = context;
            _encriptionService = encriptionService;
        }

        public void SaveTrackingData(TrackingModel tracking, HttpContext http)
        {
            tracking.Password = _encriptionService.DescriptPassword(http.Request.Cookies.FirstOrDefault(x => x.Key == "Password").Value);

            var passwordKey = _context.Passwords.ToList().LastOrDefault(p => p.PasswordValue == tracking.Password).PasswordKey;
            var visitLog = new VisitLog()
            {
                PasswordKey = passwordKey,
                VisitDate = tracking.VisitDate,
                IsVisitSuccess = tracking.IsVisitSuccess,
                IpAddress = tracking.IpAddress,
                VisitLastClickDate = tracking.VisitLastClick
            };
            var visitLogs = _context.VisitLogs.Add(visitLog);
            _context.SaveChanges();

            _context.VisitViews.Add(new VisitView() { VisitKey = visitLog.VisitKey, ViewKey = tracking.ViewKey, VisitDate = tracking.VisitDate });

            _context.SaveChanges();
        }
    }
}

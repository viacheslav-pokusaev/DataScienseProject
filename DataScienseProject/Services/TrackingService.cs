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
        private readonly IEncryptionService _encryptionService;
        private readonly DataScienceProjectDbContext _context;
        public TrackingService(DataScienceProjectDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public void SaveTrackingData(TrackingModel tracking, HttpContext http)
        {
            tracking.Password = _encryptionService.DescryptPassword(http.Request.Cookies.FirstOrDefault(x => x.Key == "Password").Value);

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

using System;

namespace DataScienseProject.Models
{
    public class TrackingModel
    {
        public int ViewKey { get; set; }
        public bool IsVisitSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public string Password { get; set; }
        public DateTime VisitLastClick { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models.Feedback
{
    public class FeedbackModel
    {        
        public int VisitKey { get; set; }
        public int ViewKey { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models.Feedback
{
    public class Feedback
    {
        public int ViewKey { get; set; }
        public string About { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

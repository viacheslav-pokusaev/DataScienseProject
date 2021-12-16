using DataScienseProject.Context;

using DataScienseProject.Interfaces;
using DataScienseProject.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class AddFeedbackService : IAddFeedbackService
    {
        private readonly DataScienceProjectDbContext _context;

        public AddFeedbackService(DataScienceProjectDbContext context)
        {
            _context = context;                
        }

        public Feedback AddFeedback(Feedback feedback)
        {            
            return new Feedback();
        }
    }
}

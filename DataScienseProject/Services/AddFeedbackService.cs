using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Feedback;

namespace DataScienseProject.Services
{
    public class AddFeedbackService : IAddFeedbackService
    {
        private readonly DataScienceProjectDbContext _context;

        public AddFeedbackService(DataScienceProjectDbContext context)
        {
            _context = context;                
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback)
        {            
            _context.Feedbacks.Add(new Feedback() { ViewKey = feedback.ViewKey, Email = feedback.Email, Text = feedback.Text });
            _context.SaveChanges();           

            return feedback;            
        }
    }
}

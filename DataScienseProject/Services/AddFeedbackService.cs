using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Feedback;

namespace DataScienseProject.Services
{
    public class AddFeedbackService : IAddFeedbackService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IEmailSenderService _emailSenderService;

        public AddFeedbackService(DataScienceProjectDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback)
        {
            _context.Feedbacks.Add(new Feedback() { ViewKey = feedback.ViewKey, Email = feedback.Email, Text = feedback.Text });
            _context.SaveChanges();

            _emailSenderService.SendEmail(null, null, feedback, EmailSendFunc.Feedback).ConfigureAwait(false);

            return feedback;
        }
    }
}

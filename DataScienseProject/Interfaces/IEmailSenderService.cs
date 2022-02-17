using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Feedback;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmail(EmailSendModel email);
        public Task SendEmailToUser(EmailSendModel email, string userEmail);
        public Task SendEmailToAdmins(EmailSendModel email, string userEmail);
        public Task SendFeedback(FeedbackModel feedback);
    }
}

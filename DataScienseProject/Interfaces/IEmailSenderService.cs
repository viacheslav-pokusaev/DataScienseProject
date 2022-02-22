using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Feedback;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmail(EmailSendModel email, string userEmail, FeedbackModel feedback, EmailSendFunc emailSendFunc);
    }
}

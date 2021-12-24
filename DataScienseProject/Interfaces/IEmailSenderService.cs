using DataScienseProject.Models;
using DataScienseProject.Models.EmailSender;

namespace DataScienseProject.Interfaces
{
    public interface IEmailSenderService
    {
        public void SendEmail(EmailSendModel email);

        public string ConfigureEmail(EmailSendModel email);
    }
}

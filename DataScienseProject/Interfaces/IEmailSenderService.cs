using DataScienseProject.Models.EmailSender;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmail(EmailSendModel email);

        public Task<string> ConfigureEmail(EmailSendModel email);
    }
}

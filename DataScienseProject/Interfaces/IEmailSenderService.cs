using DataScienseProject.Models.EmailSender;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmail(EmailSendModel email);
    }
}

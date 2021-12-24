using DataScienseProject.Interfaces;
using System.Net;
using System.Net.Mail;

namespace DataScienseProject.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public void SendEmail()
        {
            MailAddress from = new MailAddress("datasciencecs2021@gmail.com", "Portfolio");
            MailAddress to = new MailAddress("kdaniilm@gmail.com");
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Expiration password inserted";

            message.Body = "Test";
            message.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("datasciencecs2021@gmail.com", "DataScience2021");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}

using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.EmailSender;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly DataScienceProjectDbContext _context;
        public EmailSenderService(DataScienceProjectDbContext context)
        {
            _context = context;
        }

        public async Task SendEmail(EmailSendModel email)
        {
            var recipients = new List<ConfigModel>();

            recipients = _context.ConfigValues.Where(x => x.Key == "AdminEmail").Select(cv => new ConfigModel { Value = cv.Value, IsEnabled = cv.IsEnabled }).AsNoTracking().ToList();

            MailAddress from = new MailAddress("datasciencecs2021@gmail.com", "Portfolio");

            MailMessage message = new MailMessage();

            message.From = from;

            recipients.ForEach(r =>
            {
                if (r.IsEnabled)
                {
                    message.To.Add(r.Value);
                }
            });

            message.Subject = "Expiration password inserted";

            message.Body = await ConfigureEmail(email).ConfigureAwait(false);
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("datasciencecs2021@gmail.com", "DataScience2021");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }

        public async Task<string> ConfigureEmail(EmailSendModel email)
        {
            var res = "<div>";
            res += $"<h1>Try to see group: {email.GroupName}</h1>";
            res += $"<h3>Attempt time: {email.EnterTime}</h3>";
            res += $"<h3>With password: {email.Password}</h3>";
            res += "</div>";

            return await Task.Run(() => res);
        }
    }
}

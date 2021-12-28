using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.EmailSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        private readonly IConfiguration _configuration;
        public EmailSenderService(DataScienceProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task SendEmail(EmailSendModel email)
        {
            var recipients = new List<ConfigModel>();
            var smtpData = _configuration.GetSection("SmtpData");

            recipients = _context.ConfigValues.Where(x => x.Key == "AdminEmail").Select(cv => new ConfigModel { Value = cv.Value, IsEnabled = cv.IsEnabled }).AsNoTracking().ToList();

            MailAddress from = new MailAddress(smtpData.GetSection("senderEmail").Value, smtpData.GetSection("senderName").Value);

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

            message.Body = $"<div><h1>Try to see group: {email.GroupName}</h1><h3>Attempt time: {email.EnterTime}</h3><h3>With password: {email.Password}</h3></div>";
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpData.GetSection("smtpHost").Value, Convert.ToInt32(smtpData.GetSection("port").Value));

            smtp.Credentials = new NetworkCredential(smtpData.GetSection("senderEmail").Value, smtpData.GetSection("senderEmailPassword").Value);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }

    }
}

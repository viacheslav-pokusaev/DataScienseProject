using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Feedback;
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

        public async Task SendEmail(EmailSendModel email, string userEmail, FeedbackModel feedback, EmailType emailSendFunc)
        {
            MailMessage message = new MailMessage();
            var smtpData = _configuration.GetSection("SmtpData");
            MailAddress from = new MailAddress(smtpData.GetSection("senderEmail").Value, smtpData.GetSection("senderName").Value);
            message.From = from;

            if (emailSendFunc != EmailType.NewGroupToUser)
            {
                var recipients = _context.ConfigValues.Where(x => x.Key == "AdminEmail" && x.IsEnabled == true).Select(cv => new ConfigModel { Value = cv.Value, IsEnabled = cv.IsEnabled }).AsNoTracking().ToList(); ;
                
                recipients.Where(r => r.IsEnabled == true).ToList().ForEach(r =>
                {
                    message.To.Add(r.Value);
                });
            }

            switch (emailSendFunc)
            {
                case EmailType.PasswordExpire:
                    message.Subject = "Expiration password inserted";
                    message.Body = $"<div><h1>Try to see group: {email.GroupName}</h1><h3>Attempt time: {email.EnterTime}</h3><h3>With password: {email.Password}</h3></div>";
                    break;
                case EmailType.NewGroupToUser:
                    message.To.Add(userEmail);
                    message.Subject = "Access to projects";
                    message.Body = $"<div><h3>You can see our projects on the <a href={"https://gallery.customsolutions.info/" + email.GroupName}>link</a></h3><h3>With password: {email.Password}</h3></div>";
                    break;
                case EmailType.NewGroupToAdmin:
                    message.Subject = $"User get access to new group";
                    message.Body = $"<div><h1>New group name: {email.GroupName}</h1><h3>User email: {userEmail}</h3><h3>Attempt time: {email.EnterTime}</h3><h3>With password: {email.Password}</h3><h4>(User wait to your responce)</h4></div>";
                    break;
                case EmailType.Feedback:
                    message.Subject = "Get user feedback";
                    message.Body = $"<div><h1>User email: {feedback.Email}</h1><h3>Text: {feedback.Text}</h3><h3>Send feedback to view: {feedback.ViewKey}</h3></div>";
                    break;
                case EmailType.NewGroupToAdminAndUser:
                    message.Subject = $"New group was created";
                    message.Body = $"<div><h1>New group name: {email.GroupName}</h1><h3>User email: {userEmail}</h3><h3>Attempt time: {email.EnterTime}</h3><h3>With password: {email.Password}</h3><h4>(User get nessesary credentials on email)</h4></div>";
                    break;
            }

            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpData.GetSection("smtpHost").Value, Convert.ToInt32(smtpData.GetSection("port").Value));

            smtp.Credentials = new NetworkCredential(smtpData.GetSection("senderEmail").Value, smtpData.GetSection("senderEmailPassword").Value);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }
    }
}

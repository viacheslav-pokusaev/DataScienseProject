using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataScienseProject.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IEncryptionService _encryptionService;

        public AuthorizationService(DataScienceProjectDbContext context, IEmailSenderService emailSenderService, IEncryptionService encryptionService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
            _encryptionService = encryptionService;
        }
        public StatusModel CheckPasswordIsValid(AuthorizeModel authorizeModel, HttpContext http)
        {
            var res = new StatusModel();
            var pass = _context.Passwords.Join(_context.Groups, p => p.GroupKey, g => g.GroupKey, (p, g) => new
            {
                Password = p.PasswordValue,
                GroupName = g.GroupName,
                ExpirationDate = p.ExpirationDate
            }).Where(x => x.Password == authorizeModel.Password && x.GroupName == authorizeModel.GroupName).ToList().LastOrDefault();

            if (pass == null)
            {
                res.StatusCode = 403;
                res.ErrorMessage = "Password incorect";
            }
            else if (pass != null && DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(pass.ExpirationDate)) > 0)
            {
                res.StatusCode = 403;
                res.ErrorMessage = "Password expired. For continuing using service, please, contact administrator.";

                _emailSenderService.SendEmail(new EmailSendModel() { GroupName = authorizeModel.GroupName, Password = authorizeModel.Password, EnterTime = DateTime.Now }).ConfigureAwait(false);
            }
            else
            {
                res.StatusCode = 200;
                res.ErrorMessage = "";
                http.Response.Cookies.Append("Authorize", authorizeModel.GroupName, new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddMinutes(15)
                });
                http.Response.Cookies.Append("Password", _encryptionService.EncryptPassword(pass.Password), new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddMinutes(15)
                });
            }
            return res;
        }
        public StatusModel IsAuthorized(HttpContext http, string groupName)
        {
            var cookies = http.Request.Cookies.Where(x => x.Key == "Authorize" && x.Value == groupName).ToList();
            if (cookies.Count == 0)
            {
                return new StatusModel() { ErrorMessage = "", StatusCode = 403 };
            }

            return new StatusModel() { ErrorMessage = "", StatusCode = 200 };
        }

        public void UpdateCookie(HttpContext http)
        {
            foreach (var cookie in http.Request.Cookies.ToList())
            {
                http.Response.Cookies.Append(cookie.Key, cookie.Value, new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddMinutes(15)
                });
            }
        }
    }
}

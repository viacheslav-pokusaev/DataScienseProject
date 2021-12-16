using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DataScienceProjectDbContext _context;
        public AuthorizationService(DataScienceProjectDbContext context)
        {
            _context = context;
        }
        public bool CheckPass(AuthorizeModel authorizeModel, HttpContext http)
        {
            var pass = _context.Passwords.Join(_context.Groups, p => p.GroupKey, g => g.GroupKey, (p, g) => new
            {
                Password = p.PasswordValue,
                GroupName = g.GroupName,
                ExpirationDate = p.ExpirationDate
            }).Where(x => x.Password == authorizeModel.Password && x.GroupName == authorizeModel.GroupName).FirstOrDefault();

            if (pass != null)
            {
                if (DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(pass.ExpirationDate)) <= 0)
                {
                    http.Response.Cookies.Append("Authorize", authorizeModel.GroupName);
                    return true;
                }
            }
            return false;
        }
    }
}

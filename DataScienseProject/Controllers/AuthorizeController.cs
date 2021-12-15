using DataScienseProject.Context;
using DataScienseProject.Models.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly DataScienceProjectDbContext _context;
        public AuthorizeController(DataScienceProjectDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("authorize")] 
        public void Authorize(AuthorizeModel authorizeModel)
        {
            var pass = _context.Passwords.Join(_context.Groups, p => p.GroupKey, g => g.GroupKey, (p, g) => new
            {
                Password = p.PasswordValue,
                GroupName = g.GroupName,
                ExpirationDate = p.ExpirationDate
            }).FirstOrDefault();

            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(pass.ExpirationDate)) < 0)
            {
                HttpContext.Response.Cookies.Append("Authorize", authorizeModel.GroupName);
            }
        }
    }
}

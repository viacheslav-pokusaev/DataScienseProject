using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.Gallery;
using DataScienseProject.Services;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly DataScienceProjectDbContext _context;
        public AuthorizeController(IAuthorizationService authorizationService, DataScienceProjectDbContext context)
        {
            _authorizationService = authorizationService;
            _context = context;
        }
        [HttpPost]
        [Route("checkPass")]
        public ExceptionModel Authorize(AuthorizeModel authorizeModel)
        {
            if (_authorizationService.CheckPass(authorizeModel, HttpContext))
            {
                HttpContext.Response.Cookies.Append("Authorize", authorizeModel.GroupName);
                return new ExceptionModel() { ErrorMessage = "", StatusCode = 200 };
            }
            return new ExceptionModel() { ErrorMessage = "Password incorect or expired", StatusCode = 403 };
        }
    }
}

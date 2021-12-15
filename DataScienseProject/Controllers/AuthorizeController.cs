using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
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
        public void Authorize(AuthorizeModel authorizeModel)
        {
            _authorizationService.CheckPass(authorizeModel, HttpContext);
        }
    }
}

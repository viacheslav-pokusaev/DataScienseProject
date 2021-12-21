using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost]
        [Route("checkPass")]
        public StatusModel Authorize(AuthorizeModel authorizeModel)
        {
            if (_authorizationService.CheckPasswordIsValid(authorizeModel, HttpContext))
            {
                HttpContext.Response.Cookies.Append("Authorize", authorizeModel.GroupName);
                return new StatusModel() { ErrorMessage = "", StatusCode = 200 };
            }
            return new StatusModel() { ErrorMessage = "Password incorect or expired", StatusCode = 403 };
        }
    }
}

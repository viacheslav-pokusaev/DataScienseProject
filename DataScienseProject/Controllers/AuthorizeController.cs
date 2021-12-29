using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.Gallery;
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
            return _authorizationService.CheckPasswordIsValid(authorizeModel, HttpContext);
        }
    }
}

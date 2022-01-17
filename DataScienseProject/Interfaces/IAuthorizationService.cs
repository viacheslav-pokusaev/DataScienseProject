using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;

namespace DataScienseProject.Interfaces
{
    public interface IAuthorizationService
    {
        StatusModel CheckPasswordIsValid(AuthorizeModel authorizeModel, HttpContext http);

        StatusModel IsAuthorized(HttpContext http, string groupName);
        void UpdateCookie(HttpContext http);
    }
}

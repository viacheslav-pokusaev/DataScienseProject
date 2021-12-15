using DataScienseProject.Models.Authorize;
using Microsoft.AspNetCore.Http;

namespace DataScienseProject.Interfaces
{
    public interface IAuthorizationService
    {
        bool CheckPass(AuthorizeModel authorizeModel, HttpContext http);
    }
}

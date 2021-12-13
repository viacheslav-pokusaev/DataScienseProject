using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IAuthorizationService
    {
        bool CheckPass(string groupName, string Password);
    }
}

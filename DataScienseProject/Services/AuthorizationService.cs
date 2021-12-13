using DataScienseProject.Context;
using DataScienseProject.Interfaces;
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
        public bool CheckPass(string groupName, string password)
        {

            return false;
        }
    }
}

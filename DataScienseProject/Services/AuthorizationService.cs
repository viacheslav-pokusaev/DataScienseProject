﻿using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.Authorize;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataScienseProject.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DataScienceProjectDbContext _context;
        public AuthorizationService(DataScienceProjectDbContext context)
        {
            _context = context;
        }
        public bool CheckPasswordIsValid(AuthorizeModel authorizeModel, HttpContext http)
        {
            var pass = _context.Passwords.Join(_context.Groups, p => p.GroupKey, g => g.GroupKey, (p, g) => new
            {
                Password = p.PasswordValue,
                GroupName = g.GroupName,
                ExpirationDate = p.ExpirationDate
            }).Where(x => x.Password == authorizeModel.Password && x.GroupName == authorizeModel.GroupName).FirstOrDefault();

            if (pass != null && DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(pass.ExpirationDate)) <= 0)
            {
                http.Response.Cookies.Append("Authorize", authorizeModel.GroupName);
                return true;
            }
            return false;
        }

        public StatusModel IsAuthorized(HttpContext http)
        {
            var cookies = http.Request.Cookies.Where(x => x.Key == "Authorize").ToList();
            if (cookies.Count == 0)
            {
                return new StatusModel() { ErrorMessage = "Insert password", StatusCode = 403 };
            }

            return new StatusModel() { ErrorMessage = "", StatusCode = 200 };
        }
    }
}

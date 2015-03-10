using System;
using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthenticationService : IAuthenticationService
    {
        bool IAuthenticationService.IsAuthenticCredential
        {
            get
            {
                return true;
            }
        }

        bool IAuthenticationService.ValidateApplicationToken(string applicationToken, HttpContext context)
        {
            if(applicationToken == "MyValidAppToken")
            {
                return true;
            }

            return false;
        }
    }
}

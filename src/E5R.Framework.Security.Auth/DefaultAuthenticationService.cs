using System;
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

        string IAuthenticationService.createApplicationSession(string applicationToken, HttpContext context)
        {
            throw new NotImplementedException();
        }

        bool IAuthenticationService.validateApplicationToken(string applicationToken, HttpContext context)
        {
            if(applicationToken == "MyValidAppToken")
            {
                return true;
            }

            return false;
        }
    }
}
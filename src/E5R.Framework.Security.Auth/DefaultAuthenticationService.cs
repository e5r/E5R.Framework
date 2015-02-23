using System;
using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthenticationService : IAuthenticationService
    {
        private readonly ICommonDataStore<Application> _applicationStore;

        public DefaultAuthenticationService(ICommonDataStore<Application> applicationStore)
        {
            _applicationStore = applicationStore;
        }

        bool IAuthenticationService.IsAuthenticCredential
        {
            get
            {
                return true;
            }
        }

        Application IAuthenticationService.CreateApplication(Application application, HttpContext context)
        {
            throw new NotImplementedException();
        }

        string IAuthenticationService.CreateApplicationSession(string applicationToken, HttpContext context)
        {
            throw new NotImplementedException();
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
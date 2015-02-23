using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;
using System;

namespace E5R.Framework.Security.Auth
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Tells you whether a credential authentic logged
        /// 
        /// TODO: Necessary to differentiate applications and user?
        /// </summary>
        bool IsAuthenticCredential { get; }

        /// <summary>
        /// Validate a Application Token
        /// </summary>
        /// <param name="applicationToken">Application Token</param>
        /// <param name="context">HTTP context</param>
        /// <returns>True if valid</returns>
        bool ValidateApplicationToken(string applicationToken, HttpContext context);
        
        /// <summary>
        /// Create a new Session for Application
        /// </summary>
        /// <param name="applicationToken">Application Token</param>
        /// <param name="context">HTTP context</param>
        /// <returns>Application Session Token</returns>
        string CreateApplicationSession(string applicationToken, HttpContext context);

        Application CreateApplication(Application application, HttpContext context);
    }
}
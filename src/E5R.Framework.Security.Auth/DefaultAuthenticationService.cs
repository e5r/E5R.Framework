using System;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthenticationService : IAuthenticationService
    {
        bool IAuthenticationService.IsAuthenticated
        {
            get
            {
                return true;
            }
        }
    }
}
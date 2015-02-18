using System;

namespace E5R.Framework.Security.Auth
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
    }
}
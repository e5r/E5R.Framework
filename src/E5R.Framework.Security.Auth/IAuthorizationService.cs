using System;

namespace E5R.Framework.Security.Auth
{
    public interface IAuthorizationService
    {
        bool AllowUnsignedAction { get; set; }
    }
}
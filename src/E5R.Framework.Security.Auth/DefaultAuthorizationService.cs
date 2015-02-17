using System;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthorizationService : IAuthorizationService
    {
        public bool AllowUnsignedAction { get; set; } = false;
    }
}
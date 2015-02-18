using Microsoft.AspNet.Mvc;
using System;

namespace E5R.Framework.Security.Auth
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ProtectionAttribute : Attribute, IFilter
    {
        private readonly ProtectionLevel _protectionLevel;
        private readonly string[] _requiredPermissions;

        public ProtectionAttribute(ProtectionLevel protectionLevel, string[] requiredPermissions = null)
        {
            if(protectionLevel != ProtectionLevel.Private && requiredPermissions != null)
            {
                throw new ArgumentException("Only used if protectionLevel is Private", "requiredPermissions");
            }

            if(requiredPermissions != null && requiredPermissions.Length < 1)
            {
                throw new ArgumentException("At least one permission must be informed", "requiredPermissions");
            }

            _protectionLevel = protectionLevel;
            _requiredPermissions = requiredPermissions;
        }

        public bool Allowed(IAuthenticationService authenticationService, IAuthorizationService authorizationService)
        {
            if (_protectionLevel == ProtectionLevel.Public)
            {
                return true;
            }

            if(_protectionLevel == ProtectionLevel.Protected && authenticationService.IsAuthenticated)
            {
                return true;
            }

            return authorizationService.HasRequiredPermissions(_requiredPermissions);
        }
    }
}
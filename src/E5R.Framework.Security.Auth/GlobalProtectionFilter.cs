using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Linq;

namespace E5R.Framework.Security.Auth
{
    public class GlobalProtectionFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            var protectionAttributes = context.ActionDescriptor?.FilterDescriptors?
                .Where(where => where.Filter.GetType() == typeof(ProtectionAttribute))
                .Select(select => (ProtectionAttribute)select.Filter);

            var protection = protectionAttributes.FirstOrDefault();

            var authenticationService = context.HttpContext?.RequestServices?
                .GetRequiredService<IAuthenticationService>();

            var authorizationService = context.HttpContext?.RequestServices?
                .GetRequiredService<IAuthorizationService>();

            if (protection == null && authorizationService.AllowUnsignedAction)
            {
                return;
            }

            if (protection != null && protection.Allowed(authenticationService, authorizationService))
            {
                return;
            }

            base.Fail(context);
        }
    }
}

using E5R.Framework.Security.Auth;

namespace Microsoft.AspNet.Builder
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseE5RAuthServer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthenticationServerMiddleware>();
        }
    }
}

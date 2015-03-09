using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Net;
using System.Threading.Tasks;

namespace E5R.Framework.Security.Auth
{
    using static AuthenticationServerConstants;

    public class AuthenticationServerMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context/*, IAuthenticationService authenticationService*/)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<h1>E5R.AuthenticationServer not implemented!</h1>");

            // if (!context.Request.Headers.ContainsKey(HeaderApplicationToken))
            // {
            //     context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //     return;
            // }
            //
            // var applicationToken = context.Request.Headers.Get(HeaderApplicationToken);
            //
            // if (!authenticationService.ValidateApplicationToken(applicationToken, context))
            // {
            //     context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //     return;
            // }
            //
            // await _next(context);
        }
    }
}

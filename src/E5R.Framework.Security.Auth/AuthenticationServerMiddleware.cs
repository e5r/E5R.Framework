using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace E5R.Framework.Security.Auth
{
    public class AuthenticationServerMiddleware
    {
        private readonly string _headerApplicationToken = "X-Authentication-AppToken";
        private readonly string _headerSessionToken = "X-Authentication-SessionToken";
        private readonly string _createSessionRoute;
        private readonly string _createSessionMethod;
        private readonly List<string> _acceptedSchemes;

        private readonly RequestDelegate _next;

        public AuthenticationServerMiddleware(RequestDelegate next)
        {
            _next = next;
            _createSessionMethod = WebRequestMethods.Http.Post;
            _createSessionRoute = "/session";
            _acceptedSchemes = new List<string>() { "http", "https" };
        }

        public AuthenticationServerMiddleware(RequestDelegate next, string createSessionRoute) :this(next)
        {
            _createSessionRoute = createSessionRoute;
        }

        public AuthenticationServerMiddleware(RequestDelegate next, string createSessionRoute, 
            string createSessionMethod) :this(next, createSessionRoute)
        {
            _createSessionMethod = createSessionMethod;
        }

        public AuthenticationServerMiddleware(RequestDelegate next, string createSessionRoute,
            string createSessionMethod, IList<string> acceptedSchemes)
            : this(next, createSessionRoute, createSessionMethod)
        {
            _acceptedSchemes.Clear();
            _acceptedSchemes.AddRange(acceptedSchemes);
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // Only HTTPS
            if (!_acceptedSchemes.Contains(request.Scheme))
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            // HeaderApplicationToken is required
            if (!request.Headers.ContainsKey(_headerApplicationToken))
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            // HeaderSessionToken is required if not CREATE SESSION ROUTE
            if (request.Method == _createSessionMethod && request.Path.Value == _createSessionRoute)
            {
                if (!request.Headers.ContainsKey(_headerSessionToken))
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}
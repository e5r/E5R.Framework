// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using Microsoft.AspNet.Builder;
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

        public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
        {
            var requestType = HttpAuthUtils.GetRequestFluxType(context);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(requestType.ToString());

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

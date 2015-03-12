// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace E5R.Framework.Security.Auth
{
    using static Constants;
    using static HttpAuthUtils;
    using static RequestFluxType;

    public class AuthenticationServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path;

        public AuthenticationServerMiddleware(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
        }

        private async void ProcessAuthResponse(HttpContext context, HttpAuthResponse authResponse)
        {
            context.Response.StatusCode = authResponse.StatusCode;

            var acceptContentArray = context.Request.Headers.Get("Accept")?.Split(new char[] { ';' });
            var acceptContent = (acceptContentArray?.Length > 0 ? acceptContentArray[0] : "").Split(new char[] { ',' });

            if (authResponse.Body == null)
                return;

            if (acceptContent.Count(x => string.Compare(x, JsonMimeContentType, true) == 0) > 0)
            {
                context.Response.ContentType = $"{JsonMimeContentType}; charset=utf-8";
                var responseText = JsonConvert.SerializeObject(authResponse.Body);
                await context.Response.WriteAsync(responseText);
            }

            if (acceptContent.Count(x => string.Compare(x, XmlMimeContentType, true) == 0) > 0)
            {
                context.Response.ContentType = $"{XmlMimeContentType}; charset=utf-8";
                new XmlSerializer(authResponse.Body.GetType())
                    .Serialize(context.Response.Body, authResponse.Body);
            }
        }

        public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
        {
            RequestFluxType requestType = Unknown;
            HttpAuthResponse response = null;

            try
            {
                requestType = GetRequestFluxType(context, _path);

                switch (requestType)
                {
                    case RequestAccessToken:
                        // TODO: Not a /session path; Valid AppInstanceId and valid Seal
                        break;

                    case ConfirmTokenNonce:
                        // TODO: Not a /session path; Valid AccessToken and valid CNonce
                        break;

                    case ResourceRequest:
                        // TODO: Not a /session path; Valid SealedAccessToken and valid CNonce
                        // TODO: Response AccessDeny
                        break;

                    case BadRequest:
                        response = new HttpBadRequestResponse();
                        break;
                }
            }
            catch (Exception exception)
            {
                response = new HttpExceptionResponse(exception);
            }

            if(response != null)
            {
                ProcessAuthResponse(context, response);
                return;
            }

            if(requestType != ResourceRequest)
            {
                var exception = new Exception($"Internal server error when dealing request {requestType.GetType().FullName}.");
                ProcessAuthResponse(context, new HttpExceptionResponse(exception));
                return;
            }

            await _next(context);
        }
    }
}

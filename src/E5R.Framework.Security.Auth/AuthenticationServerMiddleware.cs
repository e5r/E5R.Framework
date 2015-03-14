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

        // TODO: Rename to something that let explicit authentication/session
        private readonly string _path;

        public AuthenticationServerMiddleware(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
        }

        private async void ProcessAuthResponse(HttpContext context, HttpAuthResponse authResponse)
        {
            var responseHeaders = context.Response.Headers;
            var requestHeaders = context.Request.Headers;

            // StatusCode
            context.Response.StatusCode = authResponse.StatusCode;

            // Headers
            if (!string.IsNullOrWhiteSpace(authResponse.AccessToken))
                responseHeaders.Set(HttpAuthAccessTokenHeader, authResponse.AccessToken);

            if (!string.IsNullOrWhiteSpace(authResponse.SealedAccessToken))
                responseHeaders.Set(HttpAuthSealedAccessTokenHeader, authResponse.SealedAccessToken);

            if (!string.IsNullOrWhiteSpace(authResponse.Nonce))
                responseHeaders.Set(HttpAuthNonceHeader, authResponse.Nonce);

            if (!string.IsNullOrWhiteSpace(authResponse.CNonce))
                responseHeaders.Set(HttpAuthCNonceHeader, authResponse.CNonce);

            if (!string.IsNullOrWhiteSpace(authResponse.OCNonce))
                responseHeaders.Set(HttpAuthOCNonceHeader, authResponse.OCNonce);

            // Body
            var acceptContentArray = requestHeaders.Get("Accept")?.Split(new char[] { ';' });
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
            IHeaderDictionary headers = context.Request.Headers;

            try
            {
                requestType = GetRequestFluxType(context, _path);

                if (requestType == RequestAccessToken)
                {
                    var appInstanceId = headers.Get<string>(HttpAuthAppInstanceIdHeader);
                    var seal = headers.Get<string>(HttpAuthSealHeader);

                    var accessToken = authenticationService.GetAccessToken(context, appInstanceId, seal);

                    if (accessToken == null)
                    {
                        response = new HttpUnauthorizedResponse();
                    }
                    else
                    {
                        response = new HttpAccessTokenResponse(accessToken);
                    }
                }

                if (requestType == ConfirmTokenNonce)
                {
                    var appInstanceId = headers.Get<string>(HttpAuthAppInstanceIdHeader);
                    var accessTokenValue = headers.Get<string>(HttpAuthAccessTokenHeader);
                    var cNonce = headers.Get<string>(HttpAuthCNonceHeader);

                    var accessToken = authenticationService.ConfirmToken(context, appInstanceId, accessTokenValue, cNonce);

                    if (accessToken == null)
                    {
                        response = new HttpUnauthorizedResponse();
                    }
                    else
                    {
                        response = new HttpConfirmTokenNonceResponse(accessToken);
                    }
                }

                if (requestType == ResourceRequest)
                {
                    var appInstanceId = headers.Get<string>(HttpAuthAppInstanceIdHeader);
                    var sealedAccessTokenValue = headers.Get<string>(HttpAuthSealedAccessTokenHeader);
                    var cNonce = headers.Get<string>(HttpAuthCNonceHeader);

                    if(!authenticationService.GrantAccess(context, appInstanceId, sealedAccessTokenValue, cNonce))
                    {
                        response = new HttpUnauthorizedResponse();
                    }
                }

                if (requestType == BadRequest)
                {
                    response = new HttpBadRequestResponse();
                }
            }
            catch (Exception exception)
            {
                response = new HttpExceptionResponse(exception);
            }

            if (response != null)
            {
                ProcessAuthResponse(context, response);

                return;
            }

            if (requestType != ResourceRequest)
            {
                var exception = new Exception($"Internal server error when dealing request {requestType.GetType().FullName}.");

                ProcessAuthResponse(context, new HttpExceptionResponse(exception));

                return;
            }

            await _next(context);
        }
    }
}

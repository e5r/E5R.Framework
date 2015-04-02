// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using E5R.Framework.Security.Auth.NetUtils;

namespace E5R.Framework.Security.Auth
{
    using static Constants;
    using static NetUtils.Constants;
    using static HttpAuthUtils;
    using static RequestFluxType;

    public class AuthenticationServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        // TODO: Rename to something that let explicit authentication/session
        private readonly string _path;

        public AuthenticationServerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, string path)
        {
            var loggerName = GetType().FullName.Split('.').LastOrDefault();

            _next = next;
            _logger = loggerFactory.Create(loggerName);
            _path = path;
        }

        private string convertObjectToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        private async void ProcessAuthResponse(HttpContext context, HttpAuthResponse authResponse)
        {
            _logger.WriteInformation($"ProccessAuthResponse, StatusCode: {authResponse.StatusCode}");

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
            if (authResponse.Body == null)
            {
                _logger.WriteInformation("Without body");
                return;
            }

            var acceptContentArray = requestHeaders.Get("Accept")?.Split(new char[] { ';' });
            var acceptContent = (acceptContentArray?.Length > 0 ? acceptContentArray[0] : "").Split(new char[] { ',' });

            if (acceptContent.Count(x => string.Compare(x, JsonMimeContentType, true) == 0) > 0)
            {
                _logger.WriteInformation("With a JSON body");

                context.Response.ContentType = $"{JsonMimeContentType}; charset=utf-8";

                await context.Response.WriteAsync(convertObjectToJsonString(authResponse.Body));
            }

            if (acceptContent.Count(x => string.Compare(x, XmlMimeContentType, true) == 0) > 0)
            {
                _logger.WriteWarning("With a XML body");

                context.Response.ContentType = $"{XmlMimeContentType}; charset=utf-8";

                var xmlDoc = JsonConvert.DeserializeXNode(convertObjectToJsonString(authResponse.Body), null, true);
                xmlDoc.Declaration = new XDeclaration("1.0", "utf-8", "yes");

                await context.Response.WriteAsync(xmlDoc.Declaration.ToString());
                await context.Response.WriteAsync(xmlDoc.Root.ToString());
            }
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment env,  IAuthenticationService authenticationService)
        {
            RequestFluxType requestType = Unknown;
            HttpAuthResponse response = null;
            IHeaderDictionary headers = context.Request.Headers;

            try
            {
                requestType = GetRequestFluxType(context, _path);

                if (requestType == RequestAccessToken)
                {
                    var appInstanceId = headers[HttpAuthAppInstanceIdHeader];
                    var seal = headers[HttpAuthSealHeader];

                    var accessToken = authenticationService.GetAccessToken(context.Request, appInstanceId, seal);

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
                    var appInstanceId = headers[HttpAuthAppInstanceIdHeader];
                    var accessTokenValue = headers[HttpAuthAccessTokenHeader];
                    var cNonce = headers[HttpAuthCNonceHeader];

                    var accessToken = authenticationService.ConfirmToken(context.Request, appInstanceId, accessTokenValue, cNonce);

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
                    var appInstanceId = headers[HttpAuthAppInstanceIdHeader];
                    var sealedAccessTokenValue = headers[HttpAuthSealedAccessTokenHeader];
                    var cNonce = headers[HttpAuthCNonceHeader];

                    if(!authenticationService.GrantAccess(context.Request, appInstanceId, sealedAccessTokenValue, cNonce))
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

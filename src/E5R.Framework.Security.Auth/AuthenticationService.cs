// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using E5R.Framework.Security.Auth.Data.Models;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using E5R.Framework.Security.Auth.Data;
//using Microsoft.AspNet.Http.Interfaces;

namespace E5R.Framework.Security.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataStorage<AppInstance> _appInstanceStorage;
        private readonly IDataStorage<AccessToken> _accessTokenStorage;
        private readonly ILogger _logger;
        private readonly IResourceEndPointService _resourceEndPointService;

        public AuthenticationService(ILoggerFactory loggerFactory, IResourceEndPointService resourceEndPointService, IDataStorage<AppInstance> appInstanceStorage, IDataStorage<AccessToken> accessTokenStorage)
        {
            var loggerName = GetType().FullName.Split('.').LastOrDefault();

            _logger = loggerFactory.CreateLogger(loggerName);
            _resourceEndPointService = resourceEndPointService;
            _appInstanceStorage = appInstanceStorage;
            _accessTokenStorage = accessTokenStorage;
        }

        bool IAuthenticationService.IsAuthenticCredential
        {
            get
            {
                return true;
            }
        }

        AccessToken IAuthenticationService.ConfirmToken(HttpRequest httpRequest, string appInstanceId, string accessToken, string cNonce)
        {
            var appInstance = _appInstanceStorage.Get(appInstanceId);

            if (appInstance == null)
                return null;

            // TODO: Validate client host from HttpContext
            // httpRequest.HttpContext.GetFeature<IHttpConnectionFeature>().RemoteIpAddress;

            // TODO: Validate timestamp/expired
            var accessTokenFound = _accessTokenStorage.All.SingleOrDefault(where => 
                where.AppInstance.Id == appInstance.Id && where.StringId == accessToken);

            if (accessTokenFound == null)
                return null;

            // Regenerate Token
            var sealedToken = accessTokenFound.Seal(cNonce);

            if (sealedToken == null)
                return null;

            _logger.LogInformation("SealedToken: {0}, NonceOrder: {1}", sealedToken.StringId, sealedToken.AppNonceOrder.Template);

            _accessTokenStorage.Remove(accessTokenFound);

            return _accessTokenStorage.Add(sealedToken);
        }

        AccessToken IAuthenticationService.GetAccessToken(HttpRequest httpRequest, string appInstanceId, string seal)
        {
            var appInstance = _appInstanceStorage.Get(appInstanceId);

            if (appInstance == null)
                return null;

            // TODO: Validate client host from HttpContext
            // httpRequest.HttpContext.GetFeature<IHttpConnectionFeature>().RemoteIpAddress;

            if (!appInstance.IsOriginalSeal(seal))
                return null;

            // FIXME: Remove all previours accessToken?
            _accessTokenStorage.Remove(_accessTokenStorage.All.Where(x => x.AppInstance.Id == appInstance.Id));

            var accessToken = AccessToken.Create(appInstance);

            return _accessTokenStorage.Add(accessToken);
        }

        bool IAuthenticationService.GrantAccess(HttpRequest httpRequest, string appInstanceId, string sealedAccessToken, string cNonce)
        {
            var appInstance = _appInstanceStorage.Get(appInstanceId);

            if (appInstance == null)
                return false;

            // TODO: Validate client host from HttpContext
            // httpRequest.HttpContext.GetFeature<IHttpConnectionFeature>().RemoteIpAddress;

            // TODO: Validate timestamp/expired
            var accessTokenFound = _accessTokenStorage.All.SingleOrDefault(where =>
                where.AppInstance.Id == appInstance.Id && where.StringId == sealedAccessToken && where.NonceConfirmed);

            if (accessTokenFound == null)
                return false;

            if (!accessTokenFound.ConfirmNonceByNonceOrder(cNonce))
                return false;

            // TODO: Validate app permissions

            return true;
        }
    }
}

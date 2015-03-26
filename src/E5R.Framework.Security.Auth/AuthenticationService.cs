// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using E5R.Framework.Security.Auth.Data.Models;
using Microsoft.AspNet.Http;
using E5R.Framework.Security.Auth.Data;
//using Microsoft.AspNet.Http.Interfaces;

namespace E5R.Framework.Security.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataStorage<AppInstance> _appInstanceStorage;
        private readonly IDataStorage<AccessToken> _accessTokenStorage;

        public AuthenticationService(IDataStorage<AppInstance> appInstanceStorage, IDataStorage<AccessToken> accessTokenStorage)
        {
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

            var accessTokenFound = _accessTokenStorage.All.SingleOrDefault(where => 
                where.AppInstance.Id == appInstance.Id && where.StringId == accessToken);

            if (accessTokenFound == null)
                return null;

            if (!accessTokenFound.ConfirmNonce(cNonce))
                return null;

            return _accessTokenStorage.Replace(accessTokenFound);
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

            accessToken.AppNonceOrder = appInstance.App.GetRamdonNonceOrder();

            return _accessTokenStorage.Add(accessToken);
        }

        bool IAuthenticationService.GrantAccess(HttpRequest httpRequest, string appInstanceId, string sealedAccessTokenValue, string cNonce)
        {
            throw new NotImplementedException();
        }
    }
}

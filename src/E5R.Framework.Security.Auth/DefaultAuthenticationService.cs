// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using E5R.Framework.Security.Auth.Data.Models;
using Microsoft.AspNet.Http;
using E5R.Framework.Security.Auth.Data;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthenticationService : IAuthenticationService
    {
        private readonly IDataStorage<AppInstance> _appInstanceStorage;
        private readonly IDataStorage<AccessToken> _accessTokenStorage;

        public DefaultAuthenticationService(IDataStorage<AppInstance> appInstanceStorage, IDataStorage<AccessToken> accessTokenStorage)
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

        AccessToken IAuthenticationService.ConfirmToken(HttpContext context, string appInstanceId, string accessToken, string cNonce)
        {
            throw new NotImplementedException();
        }

        AccessToken IAuthenticationService.GetAccessToken(HttpContext context, string appInstanceId, string seal)
        {
            var appInstance = _appInstanceStorage.Get(appInstanceId);

            if (appInstance == null)
                return null;

            // TODO: Validate client host from HttpContext

            // SHA(AppID:AppPrivateKey:AppInstanceHost/IP)
            if (!appInstance.IsOriginalSeal(seal))
                return null;

            // FIXME: Remove all previours accessToken?
            _accessTokenStorage.Remove(_accessTokenStorage.All.Where(x => x.AppInstance.Id == appInstance.Id));

            var accessToken = AccessToken.Create(appInstance);
            accessToken.AppNonceOrder = appInstance.App.GetRamdonNonceOrder();

            return _accessTokenStorage.Add(accessToken);
        }

        bool IAuthenticationService.GrantAccess(HttpContext context, string appInstanceId, string sealedAccessTokenValue, string cNonce)
        {
            throw new NotImplementedException();
        }
    }
}

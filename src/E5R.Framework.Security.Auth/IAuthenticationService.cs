// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;
using System;

namespace E5R.Framework.Security.Auth
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Tells you whether a credential authentic logged
        ///
        /// TODO: Move to a IClientAuthenticationService
        /// </summary>
        bool IsAuthenticCredential { get; }

        /// <summary>
        /// Validate and create a Access Token from request
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="appInstanceId"><see cref="AppInstance.Id"/></param>
        /// <param name="seal">SHA(AppID:AppPrivateKey:AppInstanceHost)</param>
        /// <returns><see cref="AccessToken"/> or null if access denied</returns>
        AccessToken GetAccessToken(HttpContext context, string appInstanceId, string seal);

        /// <summary>
        /// Validate and confirm data token from request
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="appInstanceId"><see cref="AppInstance.Id"/></param>
        /// <param name="accessToken"><see cref="AccessToken.Token"/></param>
        /// <param name="cNonce">SHA(AppID:AppPrivateKey:AppInstanceHost:Nonce)</param>
        /// <returns><see cref="AccessToken"/> or null if not confirmed</returns>
        AccessToken ConfirmToken(HttpContext context, string appInstanceId, string accessToken, string cNonce);

        /// <summary>
        /// Grant access to a resource request
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="appInstanceId"><see cref="AppInstance.Id"/></param>
        /// <param name="sealedAccessTokenValue"><see cref="AccessToken.Token"/></param>
        /// <param name="cNonce">SHA(<see cref="AppNonceOrder.Template"/>)</param>
        /// <returns><see cref="AccessToken"/> or null if no access</returns>
        bool GrantAccess(HttpContext context, string appInstanceId, string sealedAccessTokenValue, string cNonce);
    }
}

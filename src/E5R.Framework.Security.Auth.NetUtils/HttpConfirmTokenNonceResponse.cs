// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using System;
using System.Net;

namespace E5R.Framework.Security.Auth.NetUtils
{
    public class HttpConfirmTokenNonceResponse : HttpAuthResponse
    {
        public HttpConfirmTokenNonceResponse(AccessToken accessToken)
            : base((int)HttpStatusCode.Accepted)
        {
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken?.Token.ToString()))
                throw new ArgumentNullException("accessToken", "AccessToken is null");

            if (string.IsNullOrWhiteSpace(accessToken.Nonce))
                throw new NullReferenceException("Nonce is null");

            if (accessToken.AppNonceOrder == null || string.IsNullOrWhiteSpace(accessToken.AppNonceOrder.Template))
                throw new NullReferenceException("AppNonceOrder is null");

            SealedAccessToken = accessToken.Token.ToString();
            Nonce = accessToken.Nonce;
            OCNonce = accessToken.AppNonceOrder.GenerateHash(accessToken);
        }
    }
}
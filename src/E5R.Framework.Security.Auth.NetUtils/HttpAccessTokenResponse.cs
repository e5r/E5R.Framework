﻿// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using System;

namespace E5R.Framework.Security.Auth.NetUtils
{
    public class HttpAccessTokenResponse : HttpAuthResponse
    {
        public HttpAccessTokenResponse(AccessToken accessToken) 
            : base((int)System.Net.HttpStatusCode.Created)
        {
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken?.Id.ToString()))
                    throw new ArgumentNullException("accessToken", "AccessToken is null");

            if (string.IsNullOrWhiteSpace(accessToken.Nonce))
                throw new NullReferenceException("Nonce is null");

            AccessToken = accessToken.Id.ToString();
            Nonce = accessToken.Nonce;
        }
    }
}
// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using System;
using E5R.Framework.Security.Auth.Models;
using Microsoft.AspNet.Http;

namespace E5R.Framework.Security.Auth
{
    public class DefaultAuthenticationService : IAuthenticationService
    {
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
            throw new NotImplementedException();
        }

        bool IAuthenticationService.GrantAccess(HttpContext context, string appInstanceId, string sealedAccessTokenValue, string cNonce)
        {
            throw new NotImplementedException();
        }
    }
}

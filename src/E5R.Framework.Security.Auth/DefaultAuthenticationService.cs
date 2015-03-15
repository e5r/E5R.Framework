// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
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
            // TODO: Identify client host
            var clientHost = "localhost";

            // TODO: Replace temp data
            if (clientHost == "localhost" && appInstanceId == "My Test App Instance" && seal == "Erlimar seal")
            {
                var newApp = App.Create();
                var newAppInstance = AppInstance.Create(newApp);
                var newAccessToken = AccessToken.Create(newAppInstance);

                return newAccessToken;
            }

            return null;
        }

        bool IAuthenticationService.GrantAccess(HttpContext context, string appInstanceId, string sealedAccessTokenValue, string cNonce)
        {
            throw new NotImplementedException();
        }
    }
}

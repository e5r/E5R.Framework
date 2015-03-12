// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Text;

namespace E5R.Framework.Security.Auth.Models
{
    public class AppNonceOrder : DataModel<AppNonceOrder, App>
    {
        public AuthId Id { get; set; }
        public App App { get; set; }

        /// <example>
        /// {AppID}:{AppPrivateKey}:{AppInstanceHost}:{SealedAccessToken}:{Nonce}
        /// </example>
        public string Template { get; set; }
        public string GenerateHash(AccessToken accessToken)
        {
            var hashString = Template
                .Replace("{AppID}", accessToken.AppInstance.App.Id.ToString())
                .Replace("{AppPrivateKey}", accessToken.AppInstance.App.PrivateKey.ToString())
                .Replace("{AppInstancehost}", accessToken.AppInstance.Host)
                .Replace("{SealedAccessToken}", accessToken.Token.ToString())
                .Replace("{Nonce}", accessToken.Nonce);
            return Id<AlgorithmSHA1, UnicodeEncoding>.GenerateHash(hashString);
        }

        public AppNonceOrder(){}

        public AppNonceOrder(App app)
        {
            Id = new AuthId();
            App = app;
        }
    }
}

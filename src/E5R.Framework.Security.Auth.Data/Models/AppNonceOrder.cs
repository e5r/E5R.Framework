// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace E5R.Framework.Security.Auth.Data.Models
{
    public class AppNonceOrder : DataModel<AppNonceOrder, AuthId, App>
    {
        private static Random _randomEngine = new Random();

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
                .Replace("{SealedAccessToken}", accessToken.Id.ToString())
                .Replace("{Nonce}", accessToken.Nonce);
            return Id<AlgorithmSHA1, UnicodeEncoding>.GenerateHash(hashString);
        }

        private string GenerateTemplateRandom()
        {
            var template = string.Empty;
            var options = new List<string>()
            {
                "{AppID}",
                "{AppPrivateKey}",
                "{AppInstanceHost}",
                "{SealedAccessToken}",
                "{Nonce}"
            };

            while (options.Count > 0)
            {
                var index = _randomEngine.Next(0, options.Count);
                template += string.IsNullOrEmpty(template) ? "" : ":";
                template += options[index];
                options.RemoveAt(index);
            }

            return template;
        }

        public AppNonceOrder(){}

        public AppNonceOrder(App app)
        {
            Id = new AuthId();
            App = app;
            Template = GenerateTemplateRandom();
        }

        public AppNonceOrder(App app, string template)
        {
            Id = new AuthId();
            App = app;
            Template = template;
        }
    }
}

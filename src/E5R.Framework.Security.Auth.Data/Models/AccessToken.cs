// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Text;

namespace E5R.Framework.Security.Auth.Data.Models
{
    using static System.StringComparison;

    public class AccessToken : DataModel<AccessToken, AuthToken, AppInstance>
    {
        public AppInstance AppInstance { get; set; }
        public string Nonce { get; set; }
        public bool NonceConfirmed { get; set; }
        public AppNonceOrder AppNonceOrder { get; set; }

        public AccessToken Seal(string cNonce)
        {
            if (!ConfirmNonce(cNonce))
                return null;

            return new AccessToken(AppInstance)
            {
                NonceConfirmed = true,
                AppNonceOrder = AppInstance.App.GetRamdonNonceOrder()
            };
        }

        private bool ConfirmNonce(string cNonce)
        {
            var expectedCNonce = Id<AlgorithmSHA1, UnicodeEncoding>
                .GenerateHash($"{AppInstance.App.Id}:{AppInstance.App.PrivateKey}:{AppInstance.Host}:{Nonce}");

            return string.Equals(cNonce, expectedCNonce, OrdinalIgnoreCase);
        }

        public bool ConfirmNonceByNonceOrder(string cNonce)
        {
            var expectedCNonce = AppNonceOrder.GenerateNonceFromAccessToken(this);

            return string.Equals(cNonce, expectedCNonce, OrdinalIgnoreCase);
        }

        private string GenerateNonce()
        {
            var time = DateTime.UtcNow;
            var template = $"{time.ToBinary().ToString("x2")}:{time.Ticks.ToString("x2")}";
            return Id<AlgorithmSHA1, UnicodeEncoding>.GenerateHash(template);
        }

        public AccessToken(){}

        public AccessToken(AppInstance appInstance)
        {
            Id = new AuthToken();
            AppInstance = appInstance;
            Nonce = GenerateNonce();
        }
    }
}

// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

namespace E5R.Framework.Security.Auth.Models
{
    public class AccessToken : DataModel<AccessToken, AppInstance>
    {
        public AuthToken Token { get; set; }
        public AppInstance AppInstance { get; set; }
        public string Nonce { get; set; }
        public bool NonceConfirmed { get; set; }

        public AccessToken(){}

        public AccessToken(AppInstance appInstance)
        {
            Token = new AuthToken();
            AppInstance = appInstance;
        }
    }
}

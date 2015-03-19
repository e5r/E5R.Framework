// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data.Models
{
    #pragma warning disable 108
    public class App : DataModel<App, AuthId>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AuthToken PrivateKey { get; set; }

        public IEnumerable<AppNonceOrder> AppNonceOrders { get; set; }

        public AppNonceOrder GetRamdonNonceOrder()
        {
            throw new NotImplementedException();
        }

        public static App Create()
        {
            return new App()
            {
                Id = new AuthId(),
                PrivateKey = new AuthToken()
            };
        }
    }
    #pragma warning restore 108
}

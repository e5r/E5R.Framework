// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data.Models
{
    #pragma warning disable 108
    public class App : DataModel<App, AuthId>
    {
        private static Random _randomEngine = new Random();

        public string Name { get; set; }
        public string Description { get; set; }
        public AuthToken PrivateKey { get; set; }

        public IEnumerable<AppNonceOrder> NonceOrders { get; set; }

        public static App Create()
        {
            var app = new App()
            {
                Id = new AuthId(),
                PrivateKey = new AuthToken()
            };

            app.NonceOrders = GenerateNonceOrders(app);

            return app;
        }

        private static IEnumerable<AppNonceOrder> GenerateNonceOrders(App app)
        {
            yield return AppNonceOrder.Create(app);
            yield return AppNonceOrder.Create(app);
            yield return AppNonceOrder.Create(app);
            yield return AppNonceOrder.Create(app);
            yield return AppNonceOrder.Create(app);
        }

        public AppNonceOrder GetRamdonNonceOrder()
        {
            return NonceOrders.ElementAt(_randomEngine.Next(0, NonceOrders.Count()));
        }
    }
    #pragma warning restore 108
}

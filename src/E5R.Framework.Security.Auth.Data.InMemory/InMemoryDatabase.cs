// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Framework.Logging;

namespace E5R.Framework.Security.Auth.Data.InMemory
{
    public class InMemoryDatabase
    {
        private static InMemoryDatabase _instance = null;

        private readonly IDictionary<Type, dynamic> _dictionary = new Dictionary<Type, dynamic>();

        public static IList<T> GetDatabase<T>(ILogger logger)
        {
            if (_instance == null)
            {
                _instance = new InMemoryDatabase();
                Seed(logger);
            }

            if (_instance._dictionary.Count(where => where.Key == typeof(T)) != 1)
            {
                var error = new KeyNotFoundException($"Database not found for type {typeof(T).FullName}.");
                logger?.LogError(error.Message, error);
                throw error;
            }

            return (IList<T>)_instance._dictionary.SingleOrDefault(x => x.Key == typeof(T)).Value;
        }

        public InMemoryDatabase()
        {
            Alloc<AccessToken>();
            Alloc<App>();
            Alloc<AppInstance>();
            Alloc<AppNonceOrder>();
        }

        private void Alloc<T>()
        {
            if (_dictionary.Count(where => where.Key == typeof(T)) < 1)
                _dictionary.Add(typeof(T), (new List<T>()));
        }

        private static void Seed(ILogger logger)
        {
            // TODO: Move to InMemoryConfigure in Startup.cs

            var defaultApp = new App()
            {
                Id = new AuthId("4adf130c5332c69c75fba9284ce1d27e"),
                PrivateKey = new AuthToken("194cf821e066ca8708c297691ba15b16fe8c163f0ccabcf26f3eab5fd4c6779d0da6d7aef285255ae45e611c4087e081"),
                Name = "DefaultApp",
                Description = "A default App for InMemory tests"
            };

            defaultApp.NonceOrders = new List<AppNonceOrder>();

            foreach(var template in new[] {
                "{AppID}:{AppPrivateKey}:{Nonce}:{AppInstanceHost}:{SealedAccessToken}",
                "{AppID}:{Nonce}:{AppInstanceHost}:{SealedAccessToken}:{AppPrivateKey}",
                "{AppID}:{SealedAccessToken}:{Nonce}:{AppInstanceHost}:{AppPrivateKey}",
                "{AppInstanceHost}:{AppID}:{Nonce}:{AppPrivateKey}:{SealedAccessToken}",
                "{AppInstanceHost}:{SealedAccessToken}:{AppID}:{Nonce}:{AppPrivateKey}",
                "{AppInstanceHost}:{SealedAccessToken}:{Nonce}:{AppID}:{AppPrivateKey}",
                "{AppPrivateKey}:{AppInstanceHost}:{SealedAccessToken}:{AppID}:{Nonce}",
                "{AppPrivateKey}:{AppInstanceHost}:{SealedAccessToken}:{Nonce}:{AppID}",
                "{AppPrivateKey}:{Nonce}:{AppInstanceHost}:{AppID}:{SealedAccessToken}",
                "{AppPrivateKey}:{Nonce}:{SealedAccessToken}:{AppID}:{AppInstanceHost}",
                "{Nonce}:{AppInstanceHost}:{AppID}:{AppPrivateKey}:{SealedAccessToken}",
                "{Nonce}:{AppInstanceHost}:{AppPrivateKey}:{SealedAccessToken}:{AppID}",
                "{SealedAccessToken}:{AppInstanceHost}:{AppID}:{AppPrivateKey}:{Nonce}",
                "{SealedAccessToken}:{AppPrivateKey}:{AppInstanceHost}:{AppID}:{Nonce}",
                "{SealedAccessToken}:{Nonce}:{AppInstanceHost}:{AppPrivateKey}:{AppID}"
            })
            {
                (defaultApp.NonceOrders as IList<AppNonceOrder>).Add(new AppNonceOrder(defaultApp, template));
            }

            GetDatabase<App>(logger).Add(defaultApp);

            var appNonceDb = GetDatabase<AppNonceOrder>(logger);
            foreach (var order in defaultApp.NonceOrders)
                appNonceDb.Add(order);

            var defaultAppInstance = new AppInstance()
            {
                Id = new AuthId("678c588f461ca61879d2dc689f425e3f"),
                App = defaultApp,
                Host = "localhost"
            };

            GetDatabase<AppInstance>(logger).Add(defaultAppInstance);
        }
    }
}
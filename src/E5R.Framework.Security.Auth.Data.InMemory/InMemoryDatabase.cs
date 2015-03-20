// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data.InMemory
{
    public class InMemoryDatabase
    {
        private static InMemoryDatabase _instance = null;

        private readonly IDictionary<Type, dynamic> _dictionary = new Dictionary<Type, dynamic>();

        public static IList<T> GetDatabase<T>()
        {
            if (_instance == null)
            {
                _instance = new InMemoryDatabase();
                Seed();
            }

            if(_instance._dictionary.Count(where => where.Key == typeof(T)) != 1)
                throw new KeyNotFoundException($"Database not found for type {typeof(T).FullName}.");

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

        private static void Seed()
        {
            var defaultApp = App.Create();

            defaultApp.Name = "DefaultApp";
            defaultApp.Description = "A default App for InMemory tests";

            GetDatabase<App>().Add(defaultApp);

            var appNonceDb = GetDatabase<AppNonceOrder>();
            foreach (var order in defaultApp.NonceOrders)
                appNonceDb.Add(order);

            var defaultAppInstance = AppInstance.Create(defaultApp);

            defaultAppInstance.Host = "localhost";

            GetDatabase<AppInstance>().Add(defaultAppInstance);
        }
    }
}
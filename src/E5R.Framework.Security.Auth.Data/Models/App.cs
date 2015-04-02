// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// Generate a config information for use in client app.
        /// </summary>
        /// <example>
        /// {
        ///     "AppId": "#ref App.Id",
        ///     "PrivateKey": "#ref App.PrivateKey",
        ///     "NonceOrders": [
        ///         "#ref App.NonceOrders.AppNonceOrder.Template"
        ///     ]
        /// }
        /// </example>
        /// <returns>Config Information</returns>
        public string SerializedConfigInfo
        {
            get
            {
                var builder = new StringBuilder()
                    .AppendLine("{")
                    .AppendLine($"    \"AppId\": \"{ Id }\",")
                    .AppendLine($"    \"PrivateKey\": \"{ PrivateKey }\",")
                    .AppendLine($"    \"NonceOrders\": [");

                var indexMax = NonceOrders.Count() - 1;
                for (var index = 0; !(index > indexMax); index++)
                {
                    var order = NonceOrders.ElementAt(index);
                    var comma = index < indexMax ? "," : string.Empty;

                    builder.AppendLine($"        \"{ order.Template }\"{ comma }");
                }

                builder
                    .AppendLine("    ]")
                    .AppendLine("}");

                return builder.ToString();
            }
        }

        public static App Create()
        {
            var app = new App()
            {
                Id = new AuthId(),
                PrivateKey = new AuthToken()
            };

            app.GenerateNonceOrders();

            return app;
        }

        public void GenerateNonceOrders()
        {
            NonceOrders = new List<AppNonceOrder>();

            // TODO: Change to PRIVATE after InMemoryDatabase.Seed use [Startup.cs].Configure
            for (var count = 1; !(count > 15);)
            {
                var nonceOrder = AppNonceOrder.Create(this);

                if(NonceOrders.Count(where => where.Template == nonceOrder.Template) == 0)
                {
                    (NonceOrders as IList<AppNonceOrder>).Add(nonceOrder);
                    count++;
                }
            }
        }

        public AppNonceOrder GetRamdonNonceOrder()
        {
            return NonceOrders.ElementAt(_randomEngine.Next(0, NonceOrders.Count()));
        }
    }
    #pragma warning restore 108
}

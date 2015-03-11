// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using E5R.Framework.Security.Auth;
using Microsoft.AspNet.Mvc;

namespace Microsoft.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddE5RAuth(this IServiceCollection services)
        {
            services.TryAdd(E5RAuthServices.GetDefaultClientServices());

            return services;
        }

        public static IServiceCollection ConfigureE5RAuthClient(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new GlobalProtectionFilter());
            });

            return services;
        }

        public static IServiceCollection AddE5RAuthServer(this IServiceCollection services)
        {
            services.TryAdd(E5RAuthServices.GetDefaultServerServices());
            
            return services;
        }
    }
}

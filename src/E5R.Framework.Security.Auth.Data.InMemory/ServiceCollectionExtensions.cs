// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.InMemory;

namespace Microsoft.Framework.DependencyInjection
{
    using static RepositoryServices;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddE5RAuthServerInMemoryStorage(this IServiceCollection services)
        {
            services.TryAdd(GetRepositoryServices());
            
            return services;
        }
    }
}

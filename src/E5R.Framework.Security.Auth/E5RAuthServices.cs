// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using Microsoft.Framework.DependencyInjection;

namespace E5R.Framework.Security.Auth
{
    public class E5RAuthServices
    {
        public static IServiceCollection GetDefaultClientServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();

            return services;
        }

        public static IServiceCollection GetDefaultServerServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IResourceEndPointService, ResourceEndPointService>();

            return services;
        }
    }
}

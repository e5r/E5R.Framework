// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth
{
    public class E5RAuthServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultClientServices()
        {
            var describer = new ServiceDescriber();

            yield return describer.Singleton<IAuthenticationService, AuthenticationService>();
            yield return describer.Singleton<IAuthorizationService, AuthorizationService>();
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServerServices()
        {
            var describer = new ServiceDescriber();

            yield return describer.Singleton<IAuthenticationService, AuthenticationService>();
        }
    }
}

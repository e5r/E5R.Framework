// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using E5R.Framework.Security.Auth.Data.Models;
using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data.InMemory
{
    public static class RepositoryServices
    {
        public static IEnumerable<IServiceDescriptor> GetRepositoryServices()
        {
            var describer = new ServiceDescriber();

            yield return describer.Singleton<IDataStorage<AppInstance>, AppInstanceRepository>();
            yield return describer.Singleton<IDataStorage<AccessToken>, AccessTokenRepository>();
        }
    }
}

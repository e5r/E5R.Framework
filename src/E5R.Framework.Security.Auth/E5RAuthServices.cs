using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth
{
    public class E5RAuthServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            var describer = new ServiceDescriber();

            yield return describer.Singleton<IAuthorizationService, DefaultAuthorizationService>();
        }
    }
}
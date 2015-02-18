using E5R.Framework.Security.Auth;
using Microsoft.AspNet.Mvc;

namespace Microsoft.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddE5RAuth(this IServiceCollection services)
        {
            services.TryAdd(E5RAuthServices.GetDefaultServices());
            
            return services;
        }

        public static IServiceCollection ConfigureE5RAuth(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new GlobalProtectionFilter());
            });

            return services;
        }
    }
}
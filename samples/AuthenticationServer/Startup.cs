// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace AuthenticationServer
{
    using static System.StringComparison;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddE5RAuthServer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var minLogLevel = string.Equals(env.EnvironmentName, "Development", OrdinalIgnoreCase)
                ? LogLevel.Verbose
                : LogLevel.Warning;

            loggerFactory.AddConsole((category, logLevel) =>
            {
                return category == typeof(E5R.Framework.Security.Auth.AuthenticationServerMiddleware).FullName
                    && logLevel >= minLogLevel;
            });

            app.UseErrorPage();
            app.UseE5RAuthServer("/session");
        }
    }
}

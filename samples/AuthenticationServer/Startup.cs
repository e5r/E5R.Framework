// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using E5R.Framework.Security.Auth;

namespace AuthenticationServer
{
    using System;
    using System.Collections.Generic;
    using static System.StringComparison;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddE5RAuthServer()
                    .AddE5RAuthServerInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var minLogLevel = string.Equals(env.EnvironmentName, "Development", OrdinalIgnoreCase)
                ? LogLevel.Verbose
                : LogLevel.Warning;

            loggerFactory.AddConsole((category, logLevel) =>
            {
                var middlewareName = typeof(AuthenticationServerMiddleware).FullName;
                return category == middlewareName && logLevel >= minLogLevel;
            });

            app.UseErrorPage();
            app.UseE5RAuthServer("/session");
        }
    }
}

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
            services.AddE5RAuthServer()
                    .AddE5RAuthServerInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var isDev = string.Equals(env.EnvironmentName, "Development", OrdinalIgnoreCase);
            var minLogLevel = isDev ? LogLevel.Verbose : LogLevel.Warning;

            loggerFactory.AddConsole(minLogLevel);

            if (isDev)
            {
                app.UseErrorPage();
            }
            app.UseE5RAuthServer("/session");

            app.UseWelcomePage();
        }
    }
}

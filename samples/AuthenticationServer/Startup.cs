// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace AuthenticationServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddE5RAuthServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<ILoggerFactory>()
                .AddConsole((category, logLevel) =>
                {
                    return category == typeof(E5R.Framework.Security.Auth.AuthenticationServerMiddleware).FullName
#if DEBUG
                        && logLevel >= LogLevel.Verbose;
#else
                        && logLevel >= LogLevel.Warning;
#endif
                });

            app.UseErrorPage();
            app.UseE5RAuthServer("/session");
        }
    }
}

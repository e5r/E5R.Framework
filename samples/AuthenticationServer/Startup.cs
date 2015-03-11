// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

﻿using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

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
            app.UseE5RAuthServer();
        }
    }
}

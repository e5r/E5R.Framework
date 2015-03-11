// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddE5RAuth();
        services.ConfigureE5RAuthClient();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseErrorPage();
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "home", action = "index" });
        });
    }
}

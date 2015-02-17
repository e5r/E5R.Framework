using E5R.Framework.Security.Auth;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddE5RAuth();
        services.ConfigureE5RAuth();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseErrorPage();
        app.UseMvc();
    }
}

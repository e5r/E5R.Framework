using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace AuthenticationServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddE5RAuth();
            services.AddMvc();
            services.ConfigureE5RAuthServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseE5RAuthenticationServer();
            app.UseMvc();
        }
    }
}

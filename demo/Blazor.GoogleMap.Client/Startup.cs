using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.GoogleMap.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGoogleMaps(options =>
            {
                options.ApiKey = "AIzaSyDdjy-3jYU9UvXJLoTPzSyAhMH-kkiK6h4";
            });
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}

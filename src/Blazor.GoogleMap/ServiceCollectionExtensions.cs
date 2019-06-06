using Microsoft.Extensions.DependencyInjection;

namespace Blazor.GoogleMap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleMaps(this IServiceCollection services)
        {
            services.AddSingleton<GoogleMapInterop>();

            return services;
        }
    }
}

using Blazor.GoogleMap.Map.Events;
using Blazor.GoogleMap.Map.Marker;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blazor.GoogleMap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleMaps(this IServiceCollection services, Action<GoogleMapOptions> configure)
        {
            if (configure is null)
            {
                throw new ArgumentNullException($"Configuration of {nameof(GoogleMapOptions)} is required");
            }

            var googleMapOptions = new GoogleMapOptions();
            configure.Invoke(googleMapOptions);

            services.AddSingleton(googleMapOptions);
            services.AddSingleton<GoogleMapInterop>();
            services.AddSingleton<IMouseEventsInovkable, MouseEventsInvoker>();
            services.AddSingleton<MarkerCollectionFactory>();

            return services;
        }
    }
}

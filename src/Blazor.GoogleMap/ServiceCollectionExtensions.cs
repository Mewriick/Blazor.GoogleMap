using Blazor.GoogleMap.Maps.Events;
using Blazor.GoogleMap.Maps.Marker;
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

            services.AddSingleton<GoogleMapOptions>();
            services.AddSingleton<GoogleMapInterop>();
            services.AddSingleton<IMouseEventsInovkable, MouseEventsInvoker>();
            services.AddSingleton<MarkerCollectionFactory>();

            return services;
        }
    }
}

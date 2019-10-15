using Blazor.GoogleMap.Components.Rendering;
using Blazor.GoogleMap.Map.Events;
using Blazor.GoogleMap.Map.InfoWindows;
using Blazor.GoogleMap.Map.Markers;
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

            var googleMapOptions = new GoogleMapRestrictedOptions();
            configure.Invoke(googleMapOptions);

            services.AddSingleton(googleMapOptions);
            services.AddScoped<GoogleMapInterop>();
            services.AddScoped<InfoWindow>();
            services.AddScoped<IMouseEventsInovkable, MouseEventsInvoker>();
            services.AddScoped<MarkerCollectionFactory>();
            services.AddScoped<IRendereringStatusCache, MemoryRenderingCache>();

            return services;
        }
    }
}

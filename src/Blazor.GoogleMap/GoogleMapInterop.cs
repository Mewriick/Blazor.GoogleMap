using Blazor.GoogleMap.Map.Coordinates;
using Blazor.GoogleMap.Map.Events;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap
{
    public class GoogleMapInterop
    {
        private readonly IJSRuntime jSRuntime;
        private readonly IMouseEventsInovkable mouseEventsInovkable;

        public GoogleMapInterop(IJSRuntime jSRuntime, IMouseEventsInovkable mouseEventsInovkable)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
            this.mouseEventsInovkable = mouseEventsInovkable ?? throw new ArgumentNullException(nameof(mouseEventsInovkable));
        }

        public Task<bool> InitMap(LatLng latLng)
        {
            return jSRuntime.InvokeAsync<bool>("blazorGoogleMap.initMap", latLng.Lat, latLng.Lng);
        }

        public Task RegisterMouseCallbacks()
        {
            return jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.registerEventInvokers",
                new DotNetObjectRef(mouseEventsInovkable));
        }
    }
}

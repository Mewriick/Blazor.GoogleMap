using Blazor.GoogleMap.Maps.Coordinates;
using Blazor.GoogleMap.Maps.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap
{
    public class GoogleMapInterop
    {
        private readonly IJSRuntime jSRuntime;

        public GoogleMapInterop(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public Task<bool> InitMap(LatLng latLng)
        {
            return jSRuntime.InvokeAsync<bool>("blazorGoogleMap.initMap", latLng.Lat, latLng.Lng);
        }

        public Task RegisterOnClick(EventCallback<MouseEvent> onClickCallback)
        {
            return jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.registerEventInvokers",
                new DotNetObjectRef(new MouseEventsInvoker(onClickCallback)));
        }
    }
}

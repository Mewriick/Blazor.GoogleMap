using Blazor.GoogleMap.Map.Coordinates;
using Microsoft.JSInterop;
using System;

namespace Blazor.GoogleMap.Map.Marker
{
    public class Marker : JsConnectedObject, ILocationable
    {
        public Guid Id => Options.Id;

        public MarkerOptions Options { get; }

        public LatLng Position => Options.Position;

        public Marker(MarkerOptions options, IJSRuntime jSRuntime)
            : base(jSRuntime)
        {
            Options = options;
        }

        [JSInvokable]
        public void OnClickHandle()
        {
            Options.OnMarkerClick.InvokeAsync(this);
        }
    }
}

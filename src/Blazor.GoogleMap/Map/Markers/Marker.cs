using Blazor.GoogleMap.Map.Coordinates;
using Blazor.GoogleMap.Map.InfoWindows;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Markers
{
    public class Marker : JsConnectedObject, ILocationable
    {
        private readonly InfoWindow infoWindow;

        public Guid Id => Options.Id;

        public MarkerOptions Options { get; }

        public LatLng Position => Options.Position;

        public Marker(MarkerOptions options, IJSRuntime jSRuntime, InfoWindow infoWindow)
            : base(jSRuntime)
        {
            Options = options;
            this.infoWindow = infoWindow ?? throw new ArgumentNullException(nameof(infoWindow));
        }

        [JSInvokable]
        public async Task OnClickHandle()
        {
            await Options.OnMarkerClick.InvokeAsync(this);
            if (!string.IsNullOrWhiteSpace(Options.AssociatedInfoWindowId))
            {
                await infoWindow.Open(Options.AssociatedInfoWindowId, this);
            }
        }

        public Task OpenInfoWindow()
        {
            return infoWindow.Open(Options.AssociatedInfoWindowId, this);
        }
    }
}

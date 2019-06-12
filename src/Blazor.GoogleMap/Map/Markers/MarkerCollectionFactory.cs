using Blazor.GoogleMap.Map.InfoWindows;
using Microsoft.JSInterop;
using System;

namespace Blazor.GoogleMap.Map.Markers
{
    public class MarkerCollectionFactory
    {
        private readonly IJSRuntime jSRuntime;
        private readonly InfoWindow infoWindow;

        public MarkerCollectionFactory(IJSRuntime jSRuntime, InfoWindow infoWindow)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
            this.infoWindow = infoWindow ?? throw new ArgumentNullException(nameof(infoWindow));
        }

        public IMarkerCollection Create()
            => new MarkerCollection(jSRuntime, infoWindow);
    }
}

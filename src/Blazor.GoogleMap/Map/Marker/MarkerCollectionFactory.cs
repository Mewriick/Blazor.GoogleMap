using Microsoft.JSInterop;
using System;

namespace Blazor.GoogleMap.Map.Marker
{
    public class MarkerCollectionFactory
    {
        private readonly IJSRuntime jSRuntime;

        public MarkerCollectionFactory(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public IMarkerCollection Create()
            => new MarkerCollection(jSRuntime);
    }
}

using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Marker
{
    public class MarkerCollection : IMarkerCollection
    {
        private readonly Dictionary<Guid, Marker> markers;
        private readonly IJSRuntime jSRuntime;

        public int Count => markers.Count;

        public bool IsReadOnly => false;

        public Marker this[Guid markerId] => markers[markerId];

        public MarkerCollection(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
            markers = new Dictionary<Guid, Marker>();
        }

        public async Task<Marker> Add(MarkerOptions markerOptions)
        {
            var marker = new Marker(markerOptions, jSRuntime);

            await jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.addMarker",
                new DotNetObjectRef(marker), markerOptions);

            return marker;
        }

        public Task Remove(Marker marker)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Marker marker)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }


        public IEnumerator<Marker> GetEnumerator()
            => markers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => markers.Values.GetEnumerator();
    }
}

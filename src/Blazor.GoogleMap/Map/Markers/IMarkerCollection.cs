using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Markers
{
    public interface IMarkerCollection : IEnumerable<Marker>
    {
        int Count { get; }

        Marker this[Guid markerId] { get; }

        Task<Marker> Add(MarkerOptions markerOptions);

        Task<bool> Remove(Marker marker);

        bool Contains(Marker marker);

        void Clear();
    }
}

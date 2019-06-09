using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Marker
{
    public interface IMarkerCollection : IEnumerable<Marker>
    {
        Marker this[Guid markerId] { get; }

        Task<Marker> Add(MarkerOptions markerOptions);

        Task Remove(Marker marker);

        bool Contains(Marker marker);

        void Clear();
    }
}

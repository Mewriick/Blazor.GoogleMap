using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Maps.Marker
{
    public interface IMarkerCollection : IEnumerable<Marker>
    {
        Task Add(Marker marker);

        Task Remove(Marker marker);

        bool Contains(Marker marker);

        void Clear();
    }
}

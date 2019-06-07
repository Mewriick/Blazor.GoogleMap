using Blazor.GoogleMap.Maps.Coordinates;
using System;

namespace Blazor.GoogleMap.Maps.Marker
{
    public class Marker
    {
        public Guid Id { get; }

        public LatLng Position { get; }

        public Marker(LatLng position)
        {
            Id = Guid.NewGuid();
            Position = position;
        }
    }
}

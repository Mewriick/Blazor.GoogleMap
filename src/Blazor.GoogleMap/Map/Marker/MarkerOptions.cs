using Blazor.GoogleMap.Map.Coordinates;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.GoogleMap.Map.Marker
{
    public class MarkerOptions
    {
        public Guid Id { get; }

        public LatLng Position { get; }

        public string Title { get; set; } = string.Empty;

        public EventCallback<Marker> OnMarkerClick { get; set; }

        public MarkerOptions(LatLng position)
        {
            Id = Guid.NewGuid();
            Position = position;
        }
    }
}

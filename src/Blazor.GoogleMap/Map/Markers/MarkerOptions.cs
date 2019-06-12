using Blazor.GoogleMap.Map.Coordinates;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.GoogleMap.Map.Markers
{
    public class MarkerOptions
    {
        public Guid Id { get; }

        public LatLng Position { get; }

        public string Title { get; set; } = string.Empty;

        public EventCallback<Marker> OnMarkerClick { get; set; }

        /// <summary>
        /// Id of <see cref="InfoWindows.InfoWindow"/> component. If this property is not empty after <see cref="OnMarkerClick"/> <see cref="InfoWindows.InfoWindow"/> is opened
        /// </summary>
        public string AssociatedInfoWindowId { get; set; } = string.Empty;

        public MarkerOptions(LatLng position)
        {
            Id = Guid.NewGuid();
            Position = position;
        }
    }
}

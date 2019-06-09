using Blazor.GoogleMap.Map.Coordinates;
using Microsoft.AspNetCore.Components;

namespace Blazor.GoogleMap.Map.Marker
{
    public class MarkerOptions
    {
        public LatLng Position { get; }

        public string Title { get; set; } = string.Empty;

        public EventCallback<Marker> OnClick { get; set; }

        public MarkerOptions(LatLng position)
        {
            Position = position;
        }
    }
}

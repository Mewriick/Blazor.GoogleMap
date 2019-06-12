using Blazor.GoogleMap.Map.Coordinates;

namespace Blazor.GoogleMap.Map
{
    public class InitialMapOptions
    {
        public LatLng Center { get; set; } = new LatLng { Lat = -30.144, Lng = 145.25 };

        public int Zoom { get; set; } = 4;
    }
}

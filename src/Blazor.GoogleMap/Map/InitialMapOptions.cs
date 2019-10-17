using Blazor.GoogleMap.Map.Coordinates;

namespace Blazor.GoogleMap.Map
{
    public class InitialMapOptions
    {
        public LatLng Center { get; set; } = new LatLng { Lat = -30.144, Lng = 145.25 };

        public int Zoom { get; set; } = 4;

        public string Width { get; set; } = "600px";

        public string Height { get; set; } = "600px";

        public bool ZoomControl { get; set; }

        public bool MapTypeControl { get; set; }

        public bool ScaleControl { get; set; }

        public bool RotateControl { get; set; }

        public bool FullscreenControl { get; set; } = true;

        public bool StreetViewControl { get; set; }

        public bool Scrollwheel { get; set; } = true;

        public string MapTypeId { get; set; } = MapTypes.Roadmap;

    }

    public static class MapTypes
    {
        public readonly static string Roadmap = "roadmap";
        public readonly static string Satellite = "satellite";
        public readonly static string Hybrid = "hybrid";
        public readonly static string Terrain = "terrain";
    }
}

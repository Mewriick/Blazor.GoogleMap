namespace Blazor.GoogleMap.Map.Coordinates
{
    public class LatLng
    {
        public double Lat { get; set; }

        public double Lng { get; set; }

        public override string ToString()
        {
            return $"Lat: {Lat}, Lng: {Lng}";
        }
    }
}

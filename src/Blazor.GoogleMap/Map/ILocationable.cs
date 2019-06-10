using Blazor.GoogleMap.Map.Coordinates;

namespace Blazor.GoogleMap.Map
{
    public interface ILocationable
    {
        LatLng Position { get; }
    }
}

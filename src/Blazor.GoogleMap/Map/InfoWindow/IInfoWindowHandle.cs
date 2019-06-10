using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.InfoWindow
{
    public interface IInfoWindowHandle
    {
        Task Open(string infoWindowId, ILocationable locationable = null);

        Task Close(string infoWindowId);
    }
}

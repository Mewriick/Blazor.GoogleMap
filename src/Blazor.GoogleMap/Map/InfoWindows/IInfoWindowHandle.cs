using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.InfoWindows
{
    public interface IInfoWindowHandle
    {
        Task Open(string infoWindowId, ILocationable locationable);

        Task Open(MarkupString htmlContent, ILocationable locationable);
    }
}

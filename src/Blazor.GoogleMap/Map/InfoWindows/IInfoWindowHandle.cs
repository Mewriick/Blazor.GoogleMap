using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.InfoWindows
{
    public interface IInfoWindowHandle
    {
        Task<object> Open(string infoWindowId, ILocationable locationable);

        Task<object> Open(MarkupString htmlContent, ILocationable locationable);
    }
}

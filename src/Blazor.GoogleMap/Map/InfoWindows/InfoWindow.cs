using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.InfoWindows
{
    public class InfoWindow : JsConnectedObject, IInfoWindowHandle
    {
        public InfoWindow(IJSRuntime jSRuntime)
            : base(jSRuntime)
        {
        }

        public async Task<object> Open(string infoWindowId, ILocationable locationable = null)
        {
            return await jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.openInfoWindow", infoWindowId, locationable);
        }

        public async Task<object> Open(MarkupString htmlContent, ILocationable locationable)
        {
            return await jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.openInfoWindow", string.Empty, locationable, htmlContent.ToString());
        }
    }
}

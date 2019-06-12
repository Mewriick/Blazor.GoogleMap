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

        public Task Open(string infoWindowId, ILocationable locationable = null)
        {
            return jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.openInfoWindow", infoWindowId, locationable);
        }

        public Task Open(MarkupString htmlContent, ILocationable locationable)
        {
            return jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.openInfoWindow", string.Empty, locationable, htmlContent.ToString());
        }
    }
}

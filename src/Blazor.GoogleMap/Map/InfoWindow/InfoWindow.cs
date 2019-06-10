using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.InfoWindow
{
    public class InfoWindow : JsConnectedObject, IInfoWindowHandle
    {
        public InfoWindow(IJSRuntime jSRuntime)
            : base(jSRuntime)
        {
        }

        public Task Close(string infoWindowId)
        {
            throw new NotImplementedException();
        }

        public Task Open(string infoWindowId, ILocationable locationable = null)
        {
            return jSRuntime.InvokeAsync<object>(
                "blazorGoogleMap.openInfoWindow", infoWindowId, locationable);
        }
    }
}

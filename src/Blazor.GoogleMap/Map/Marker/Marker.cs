using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Marker
{
    public class Marker : JsConnectedObject
    {
        public Guid Id { get; }

        public MarkerOptions Options { get; }

        public Marker(MarkerOptions options, IJSRuntime jSRuntime)
            : base(jSRuntime)
        {
            Id = Guid.NewGuid();
            Options = options;
        }

        [JSInvokable]
        private Task OnClickHandle()
        {
            return Options?.OnClick.InvokeAsync(this);
        }
    }
}

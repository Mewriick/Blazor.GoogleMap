using Microsoft.JSInterop;
using System;

namespace Blazor.GoogleMap.Map
{
    public abstract class JsConnectedObject
    {
        protected readonly IJSRuntime jSRuntime;

        public JsConnectedObject(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }
    }
}

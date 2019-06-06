using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Maps.Events
{
    public class MouseEventsInvoker
    {
        private readonly EventCallback<MouseEvent> onClickCallback;

        public MouseEventsInvoker(EventCallback<MouseEvent> onClickCallback)
        {
            this.onClickCallback = onClickCallback;
        }

        [JSInvokable]
        public async Task InvokeOnClick(MouseEvent mouseEvent)
        {
            await onClickCallback.InvokeAsync(mouseEvent);
        }
    }
}

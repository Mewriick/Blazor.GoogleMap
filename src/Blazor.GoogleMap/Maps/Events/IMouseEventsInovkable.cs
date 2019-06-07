using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Maps.Events
{
    public interface IMouseEventsInovkable
    {
        IMouseEventsInovkable RegisterCallback(MouseEvent mouseEvent, EventCallback<MouseEventArgs> eventCallback);

        Task InvokeMouseEvent(string eventName, MouseEventArgs mouseEventArgs);
    }
}

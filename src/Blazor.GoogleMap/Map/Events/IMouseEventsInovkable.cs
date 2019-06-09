using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Events
{
    public interface IMouseEventsInovkable
    {
        IMouseEventsInovkable RegisterCallback(MouseEvent mouseEvent, EventCallback<MouseEventArgs> eventCallback);

        Task InvokeMouseEvent(string eventName, MouseEventArgs mouseEventArgs);
    }
}

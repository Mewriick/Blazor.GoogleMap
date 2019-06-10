using Microsoft.JSInterop;
using System;

namespace Blazor.GoogleMap.Map
{
    public class InitMapCallback
    {
        public Action AfterCallbackAction { get; }

        public InitMapCallback(Action afterCallbackAction)
        {
            AfterCallbackAction = afterCallbackAction;
        }

        [JSInvokable]
        public void OnInitFinished()
        {
            AfterCallbackAction.Invoke();
        }
    }
}

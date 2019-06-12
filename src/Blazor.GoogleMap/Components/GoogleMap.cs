using Blazor.GoogleMap.Components.Rendering;
using Blazor.GoogleMap.Map;
using Blazor.GoogleMap.Map.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Components
{
    [Route("/googlemap")]
    public class GoogleMap : ComponentBase
    {
        private bool mapWasInitialized;

        [Inject] GoogleMapRestrictedOptions GoogleMapOptions { get; set; }

        [Inject] GoogleMapInterop GoogleMapInterop { get; set; }

        [Inject] IMouseEventsInovkable MouseEventsInovkable { get; set; }

        [Parameter] string Id { get; set; } = "map";

        [Parameter] string CssClasses { get; set; } = "map";

        [Parameter] InitialMapOptions InitialMapOptions { get; set; } = new InitialMapOptions();

        [Parameter] EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

        public GoogleMap()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            var internalBuilder = new BlazorRendererTreeBuilder(builder);

            internalBuilder
                .OpenElement("div")
                .AddAttribute("id", Id)
                .AddAttribute("class", CssClasses)
                .OpenElement("script")
                .AddAttribute("async", "")
                .AddAttribute("defer", "")
                .AddAttribute("src", GoogleMapOptions.MapJsWasIncluded
                    ? string.Empty
                    : $"https://maps.googleapis.com/maps/api/js?key={GoogleMapOptions.ApiKey}&callback=blazorGoogleMap.initMapCallback")
                .CloseElement()
                .CloseElement();
        }

        protected override async Task OnInitAsync()
        {
            MouseEventsInovkable
                .RegisterCallback(MouseEvent.Click, OnClick)
                .RegisterCallback(MouseEvent.DblClick, OnDoubleClick);

            await GoogleMapInterop.RegisterCallbacks(new Map.InitMapCallback(MapInitialized));
            await GoogleMapInterop.InitMap(InitialMapOptions);
        }


        protected override Task OnAfterRenderAsync()
        {
            if (GoogleMapOptions.MapJsWasIncluded && !mapWasInitialized)
            {
                return GoogleMapInterop.ExecuteInitMapCallback();
            }

            return Task.CompletedTask;
        }

        private void MapInitialized()
        {
            GoogleMapOptions.MapJsWasIncluded = true;
            mapWasInitialized = true;
        }
    }
}

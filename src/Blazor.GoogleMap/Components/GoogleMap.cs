using Blazor.GoogleMap.Components.Rendering;
using Blazor.GoogleMap.Map;
using Blazor.GoogleMap.Map.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
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

        [Parameter] public string Id { get; set; } = "map";

        [Parameter] public string CssClasses { get; set; } = "map";

        [Parameter] public InitialMapOptions InitialMapOptions { get; set; } = new InitialMapOptions();

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

        public GoogleMap()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            var internalBuilder = new BlazorRendererTreeBuilder(builder);

            if(string.IsNullOrEmpty(GoogleMapOptions.Width) || string.IsNullOrEmpty(GoogleMapOptions.Height))
            {
                throw new ArgumentNullException("Map width and height must have some values.");
            }

            internalBuilder
                .OpenElement("div")
                .AddAttribute("style", $"width: {GoogleMapOptions.Width}; height: {GoogleMapOptions.Height}")
                .AddAttribute("id", Id)
                .AddAttribute("class", CssClasses)
                .OpenElement("script")
                .AddAttribute("async", "")
                .AddAttribute("defer", "")
                .AddAttribute("src", !string.IsNullOrEmpty(GoogleMapOptions.ApiKey)
                    ? $"https://maps.googleapis.com/maps/api/js?key={GoogleMapOptions.ApiKey}&callback=blazorGoogleMap.initMapCallback"
                    : string.Empty
                    )
                .CloseElement()
                .CloseElement();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {

            MouseEventsInovkable
             .RegisterCallback(MouseEvent.Click, OnClick)
             .RegisterCallback(MouseEvent.DblClick, OnDoubleClick);

            await GoogleMapInterop.RegisterCallbacks(new InitMapCallback(MapInitialized));
            await GoogleMapInterop.InitMap(InitialMapOptions);


            if (!mapWasInitialized && firstRender)
            {
                await GoogleMapInterop.ExecuteInitMapCallback();
                mapWasInitialized = true;
            }
        }

        private void MapInitialized()
        {
            //TODO: Do some extra operations if needed when map is initialized          
        }
    }
}

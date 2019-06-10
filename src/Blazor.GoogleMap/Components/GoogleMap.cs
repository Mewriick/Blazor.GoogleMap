using Blazor.GoogleMap.Map.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Components
{
    [Route("/googlemap")]
    public class GoogleMap : ComponentBase
    {
        [Inject] GoogleMapOptions GoogleMapOptions { get; set; }

        [Inject] GoogleMapInterop GoogleMapInterop { get; set; }

        [Inject] IMouseEventsInovkable MouseEventsInovkable { get; set; }

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
                .AddAttribute("id", "map")
                .OpenElement("script")
                .AddAttribute("async", "")
                .AddAttribute("defer", "")
                .AddAttribute("src", $"https://maps.googleapis.com/maps/api/js?key={GoogleMapOptions.ApiKey}&callback=blazorGoogleMap.initMapCallback")
                .CloseElement()
                .CloseElement();
        }

        protected override async Task OnInitAsync()
        {
            MouseEventsInovkable
                .RegisterCallback(MouseEvent.Click, OnClick)
                .RegisterCallback(MouseEvent.DblClick, OnDoubleClick);

            await GoogleMapInterop.RegisterCallbacks(new Map.InitMapCallback(MapInitialized));
            await GoogleMapInterop.InitMap(
                new Map.Coordinates.LatLng
                {
                    Lat = -30.144,
                    Lng = 145.25
                });
        }

        private void MapInitialized()
        {

        }
    }
}

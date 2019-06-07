using Blazor.GoogleMap.Maps.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Components
{
    [Route("/googlemap")]
    public class GoogleMap : ComponentBase
    {
        [Inject] GoogleMapInterop GoogleMapInterop { get; set; }

        [Inject] IMouseEventsInovkable MouseEventsInovkable { get; set; }

        [Parameter] EventCallback<MouseEventArgs> OnClick { get; set; }

        public GoogleMap()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            base.BuildRenderTree(builder);

            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "id", "map");
            builder.OpenElement(++seq, "script");
            builder.AddAttribute(++seq, "async", "");
            builder.AddAttribute(++seq, "defer", "");
            builder.AddAttribute(++seq, "src", "https://maps.googleapis.com/maps/api/js?key=AIzaSyDdjy-3jYU9UvXJLoTPzSyAhMH-kkiK6h4&callback=blazorGoogleMap.initMapCallback");
            builder.CloseElement();
            builder.CloseElement();
        }

        protected override async Task OnInitAsync()
        {
            MouseEventsInovkable.RegisterCallback(MouseEvent.Click, OnClick);

            await GoogleMapInterop.RegisterMouseCallbacks();
            await GoogleMapInterop.InitMap(
                new Maps.Coordinates.LatLng
                {
                    Lat = -30.144,
                    Lng = 145.25
                });
        }
    }
}

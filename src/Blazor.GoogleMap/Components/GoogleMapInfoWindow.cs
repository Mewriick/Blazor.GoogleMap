using Blazor.GoogleMap.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;

namespace Blazor.GoogleMap.Components
{
    [Route("/googlemapinfowindow")]
    public class GoogleMapInfoWindow : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Id { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            var internalBuilder = new BlazorRendererTreeBuilder(builder);
            internalBuilder
                .OpenElement("div")
                .AddAttribute("id", Id)
                .AddAttribute("style", "display: none")
                .AddContent(ChildContent)
                .CloseElement();
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new InvalidOperationException($"{nameof(GoogleMapInfoWindow)} require a {nameof(Id)} parameter.");
            }
        }
    }
}

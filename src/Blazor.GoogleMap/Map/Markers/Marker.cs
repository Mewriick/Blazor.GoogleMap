using Blazor.GoogleMap.Map.Coordinates;
using Blazor.GoogleMap.Map.Events;
using Blazor.GoogleMap.Map.InfoWindows;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.GoogleMap.Map.Markers
{
    public class Marker : JsConnectedObject, ILocationable
    {
        private readonly InfoWindow infoWindow;

        public Guid Id { get; }

        public MarkerOptions Options { get; }

        public LatLng Position => Options.Position;

        public Marker(MarkerOptions options, IJSRuntime jSRuntime, InfoWindow infoWindow)
            : base(jSRuntime)
        {
            Id = Guid.NewGuid();
            Options = options ?? throw new ArgumentNullException(nameof(options));
            this.infoWindow = infoWindow ?? throw new ArgumentNullException(nameof(infoWindow));
        }

        [JSInvokable]
        public Task OnClickHandle()
        {
            if (!Options.OnMarkerClick.HasDelegate)
            {
                return Task.CompletedTask;
            }

            return Task.WhenAll(
                Options.OnMarkerClick.InvokeAsync(this),
                !string.IsNullOrWhiteSpace(Options.AssociatedInfoWindowId)
                 ? infoWindow.Open(Options.AssociatedInfoWindowId, this)
                 : Task.CompletedTask
            );
        }

        [JSInvokable]
        public Task OnDragEndHandle(MouseEventArgs mouseEventArgs)
        {
            Options.SetPosition(mouseEventArgs.LatLng);
            if (Options.OnDragEnd.HasDelegate)
            {
                return Options.OnDragEnd.InvokeAsync(this);
            }

            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task OnRightClickHandle()
        {
            if (Options.OnMarkerRightClick.HasDelegate)
            {
                return Options.OnMarkerRightClick.InvokeAsync(this);
            }

            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task OnDoubleClickHandle()
        {
            if (Options.OnMarkerDblClick.HasDelegate)
            {
                return Options.OnMarkerDblClick.InvokeAsync(this);
            }

            return Task.CompletedTask;
        }

        public async Task SetAnimation(MarkerAnimation markerAnimation)
        {
            Options.Animation = markerAnimation;
            await jSRuntime.InvokeAsync<bool>(
                "blazorGoogleMap.markersModule.setAnimation", Id, markerAnimation);
        }

        public async Task<bool> SetIcon(string iconUrl)
        {
            Options.Icon = iconUrl;
            return await jSRuntime.InvokeAsync<bool>(
                "blazorGoogleMap.markersModule.setIcon", Id, iconUrl);
        }

        public async Task<bool> SetOpacity(double opacity)
        {
            Options.Opacity = opacity;
            return await jSRuntime.InvokeAsync<bool>(
                 "blazorGoogleMap.markersModule.setOpacity", Id, opacity);

        }

        public async Task<bool> SetVisibility(bool visible)
        {
            Options.Visible = visible;
            return await jSRuntime.InvokeAsync<bool>(
                "blazorGoogleMap.markersModule.setVisibility", Id, visible);
        }

        public Task OpenInfoWindow(string infoWindowId)
        {
            infoWindowId = string.IsNullOrWhiteSpace(infoWindowId)
                ? Options.AssociatedInfoWindowId
                : infoWindowId;

            if (!string.IsNullOrWhiteSpace(infoWindowId))
            {
                return infoWindow.Open(infoWindowId, this);
            }

            return Task.CompletedTask;
        }

        public Task OpenInfoWindow(MarkupString content)
            => infoWindow.Open(content, this);
    }
}

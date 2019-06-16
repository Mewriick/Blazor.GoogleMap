using Blazor.GoogleMap.Map.Coordinates;
using Microsoft.AspNetCore.Components;

namespace Blazor.GoogleMap.Map.Markers
{
    public class MarkerOptions
    {
        public LatLng Position { get; private set; }

        public string Title { get; set; } = string.Empty;

        public MarkerLabel Label { get; set; }

        public string Icon { get; set; } = string.Empty;

        public MarkerAnimation Animation { get; set; } = MarkerAnimation.None;

        /// <summary>
        /// The marker's opacity between 0.0 and 1.0.
        /// </summary>
        public double Opacity { get; set; } = 1.0;

        public bool Visible { get; set; } = true;

        public bool Draggable { get; set; }

        public bool Clickable { get; set; } = true;

        public string Cursor { get; set; }

        public EventCallback<Marker> OnMarkerClick { get; set; }

        public EventCallback<Marker> OnMarkerDblClick { get; set; }

        public EventCallback<Marker> OnMarkerRightClick { get; set; }

        public EventCallback<Marker> OnDragEnd { get; set; }

        /// <summary>
        /// Id of <see cref="InfoWindows.InfoWindow"/> component. If this property is not empty after <see cref="OnMarkerClick"/> <see cref="InfoWindows.InfoWindow"/> is opened
        /// </summary>
        public string AssociatedInfoWindowId { get; set; } = string.Empty;

        public MarkerOptions(LatLng position)
        {
            Position = position;
        }

        internal void SetPosition(LatLng position)
        {
            Position = position;
        }
    }
}

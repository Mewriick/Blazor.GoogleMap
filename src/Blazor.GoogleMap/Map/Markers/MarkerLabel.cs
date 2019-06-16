using System;

namespace Blazor.GoogleMap.Map.Markers
{
    public class MarkerLabel
    {
        public string Text { get; }

        /// <summary>
        /// The color of the label text. Default color is black.
        /// </summary>
        public string Color { get; set; } = "black";

        /// <summary>
        /// The font family of the label text (equivalent to the CSS font-family property).
        /// </summary>
        public string FontFamliy { get; set; } = string.Empty;

        /// <summary>
        /// The font size of the label text (equivalent to the CSS font-size property). Default size is 14px.
        /// </summary>
        public string FontSize { get; set; } = "14px";

        /// <summary>
        /// The font weight of the label text (equivalent to the CSS font-weight property).
        /// </summary>
        public string FontWeight { get; set; } = string.Empty;

        public MarkerLabel(string text)
        {
            Text = string.IsNullOrWhiteSpace(text)
                ? throw new ArgumentNullException(nameof(text))
                : text;
        }
    }
}

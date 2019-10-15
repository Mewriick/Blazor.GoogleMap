namespace Blazor.GoogleMap
{
    public class GoogleMapOptions
    {
        public string ApiKey { get; set; }
        public string Width { get; set; } = "600px";
        public string Height { get; set; } = "600px";

    }

    internal class GoogleMapRestrictedOptions : GoogleMapOptions
    {
        public bool MapJsWasIncluded { get; set; }
    }
}

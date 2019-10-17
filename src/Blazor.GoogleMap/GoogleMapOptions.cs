namespace Blazor.GoogleMap
{
    public class GoogleMapOptions
    {
        public string ApiKey { get; set; }

    }

    internal class GoogleMapRestrictedOptions : GoogleMapOptions
    {
        public bool MapJsWasIncluded { get; set; }
    }
}

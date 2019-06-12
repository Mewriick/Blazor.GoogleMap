namespace Blazor.GoogleMap.Components.Rendering
{
    public interface IRendereringStatusCache
    {
        RenderingStatus this[string componentKey] { get; }

        void AddRenderingStatus(string componentKey, RenderingStatus renderingStatus);
    }
}

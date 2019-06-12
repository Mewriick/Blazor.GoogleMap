using System.Collections.Concurrent;

namespace Blazor.GoogleMap.Components.Rendering
{
    public class MemoryRenderingCache : IRendereringStatusCache
    {
        ConcurrentDictionary<string, RenderingStatus> renderingStatusCache = new ConcurrentDictionary<string, RenderingStatus>();

        public RenderingStatus this[string componentKey]
        {
            get
            {
                return renderingStatusCache.ContainsKey(componentKey)
                    ? renderingStatusCache[componentKey]
                    : RenderingStatus.Idle;
            }
        }

        public void AddRenderingStatus(string componentKey, RenderingStatus renderingStatus)
        {
            renderingStatusCache.AddOrUpdate(componentKey, renderingStatus, (key, previewState) => renderingStatus);
        }
    }
}

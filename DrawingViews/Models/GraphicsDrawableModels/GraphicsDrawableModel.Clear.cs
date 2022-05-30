using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IGraphicsDrawable
{
    public void Clear()
    {
        lock (Drawings)
        {
            foreach (var i in Drawings)
            {
                i.Dispose();
            }
            Drawings.Clear();
        }
    }
}

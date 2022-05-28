namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IGraphicsDrawable
{
    public void Erase(IDrawableShape drawing)
    {
        lock (Drawings)
        {
            Drawings.Remove(drawing);
        }
    }
}

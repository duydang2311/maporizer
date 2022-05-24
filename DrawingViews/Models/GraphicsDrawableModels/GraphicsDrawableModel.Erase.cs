namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IDrawable
{
    public void Erase(IDrawableShape drawing)
    {
        lock (Drawings)
        {
            Drawings.Remove(drawing);
        }
    }
}

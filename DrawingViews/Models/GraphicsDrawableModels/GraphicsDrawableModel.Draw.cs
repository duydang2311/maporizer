namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IDrawable
{
    public void Draw(IDrawableShape drawing)
    {
        lock (Drawings)
        {
            Drawings.AddFirst(drawing);
        }
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Antialias = true;
        lock (Drawings)
        {
            foreach (var drawing in Drawings)
            {
                drawing.Draw(canvas, dirtyRect);
            }
        }
    }
}


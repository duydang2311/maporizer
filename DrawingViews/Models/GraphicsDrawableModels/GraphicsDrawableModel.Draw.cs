namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : Microsoft.Maui.Graphics.IDrawable
{
    public void Draw(IDrawable drawing)
    {
        lock (Drawings)
        {
            Drawings.AddLast(drawing);
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


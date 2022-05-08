namespace Maporizer.DrawingViews.Models;

public class GraphicsDrawable : Microsoft.Maui.Graphics.IDrawable
{
    public readonly LinkedList<IDrawable> Drawings;
    public GraphicsDrawable() {
        Drawings = new();
    }
    public void Draw(IDrawable drawing)
    {
        Drawings.AddLast(drawing);
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        lock (Drawings)
        {
            canvas.Antialias = true;
            foreach (var drawing in Drawings)
            {
                drawing.Draw(canvas, dirtyRect);
            }
        }
    }
}


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


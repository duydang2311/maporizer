namespace Maporizer.ViewModels;

public class GraphicsDrawable : IDrawable
{
    public GraphicsDrawable()
    {
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Antialias = true;
        canvas.StrokeColor = Colors.White;

        canvas.DrawEllipse(100, 100, 100, 100);
    }
}


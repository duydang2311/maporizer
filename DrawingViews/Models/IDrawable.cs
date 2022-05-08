namespace Maporizer.DrawingViews.Models;

public interface IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect);
}

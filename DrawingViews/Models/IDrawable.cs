namespace Maporizer.DrawingViews.Models;

public interface IDrawable
{
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect);
    public bool IsCollidedWith(PointF point);
    public void Scale(float scale);
}

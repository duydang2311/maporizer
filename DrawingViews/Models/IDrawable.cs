namespace Maporizer.DrawingViews.Models;

public interface IDrawable
{
    public Point Location { get; set; }
    public Size Size { get; set; }
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect);
    public bool IsCollidedWith(Point point);
    public bool IsCollidedWith(PointF point);
}

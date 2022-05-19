namespace Maporizer.DrawingViews.Models;

public interface IDrawable
{
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public bool Ignored { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect);
    public bool HasPointOn(PointF point, float? epsilon = null);
    public bool HasPointIn(PointF point);
    public PointF? GetIntersectionPoint(PointF point, float? epsilon = null);
    public void Scale(float scale);
}

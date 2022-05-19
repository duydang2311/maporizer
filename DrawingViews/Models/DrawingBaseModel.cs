namespace Maporizer.DrawingViews.Models;

public abstract class DrawingBaseModel : IDrawable
{
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public bool Ignored { get; set; }
    public DrawingBaseModel()
    {
        StrokeColor = Colors.Transparent;
        FillColor = Colors.Transparent;
        Ignored = false;
    }
    public abstract void Draw(ICanvas canvas, RectF dirtyRect);
    public abstract bool HasPointOn(PointF point, float? epsilon = null);
    public abstract bool HasPointIn(PointF point);
    public abstract PointF? GetIntersectionPoint(PointF point, float? epsilon = null);
    public abstract void Scale(float scale);
}

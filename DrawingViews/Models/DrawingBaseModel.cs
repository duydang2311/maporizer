namespace Maporizer.DrawingViews.Models;

public abstract class DrawingBaseModel : IDrawable
{
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public DrawingBaseModel()
    {
        StrokeColor = Colors.Transparent;
        FillColor = Colors.Transparent;
    }
    public abstract void Draw(ICanvas canvas, RectF dirtyRect);
    public abstract bool IsCollidedWith(PointF point, bool solid = false, float? epsilon = null);
    public abstract PointF? GetIntersectionPoint(PointF point, float? epsilon = null);
    public abstract void Scale(float scale);
}

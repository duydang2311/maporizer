namespace Maporizer.DrawingViews.Models;

public interface IDrawableShape : IDisposable
{
    PathF Path { get; }
    Color StrokeColor { get; set; }
    Color FillColor { get; set; }
    bool Ignored { get; set; }
    void Draw(ICanvas canvas, RectF dirtyRect);
    bool HasPointOn(PointF point, float? epsilon = null);
    bool HasPointIn(PointF point);
    PointF? GetIntersectionPoint(PointF point, float? epsilon = null);
    void Scale(float scale);
    void Translate(SizeF offset);
}

namespace Maporizer.DrawingViews.Models;

public abstract class DrawingBaseModel : IDrawableShape
{
    protected bool disposed;
    public PathF Path { get; protected set; }

    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public bool Ignored { get; set; }
    public DrawingBaseModel()
    {
        Path = new PathF();
        StrokeColor = Colors.Transparent;
        FillColor = Colors.Transparent;
        Ignored = false;
    }
    public abstract void Draw(ICanvas canvas, RectF dirtyRect);
    public abstract bool HasPointOn(PointF point, float? epsilon = null);
    public abstract bool HasPointIn(PointF point);
    public abstract PointF? GetIntersectionPoint(PointF point, float? epsilon = null);
    public abstract void Scale(float scale);
    public abstract void Translate(SizeF offset);
    protected abstract void Dispose(bool disposing);
    ~DrawingBaseModel()
    {
        Dispose(disposing: false);
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

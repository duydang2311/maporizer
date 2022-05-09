namespace Maporizer.DrawingViews.Models;

public class EllipseModel : DrawingBaseModel
{
    public EllipseModel() : base() { }
    public override void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Rect rect = new(Location, Size);
        canvas.StrokeSize = (float)Size.Width / 25f;
        canvas.StrokeColor = StrokeColor;
        canvas.FillColor = FillColor;
        canvas.FillEllipse(rect);
        canvas.DrawEllipse(rect);
    }
    public override bool IsCollidedWith(PointF point)
    {
        return IsCollidedWith((Point)point);
    }
    public override bool IsCollidedWith(Point point)
    {
        var radius = (Size.Width + Size.Width / 25f) / 2;
        var center = new Point(Location.X + radius, Location.Y + radius);
        var dx = point.X - center.X;
        var dy = point.Y - center.Y;
        var distanceSquared = dx * dx + dy * dy;
        return distanceSquared <= radius * radius;
    }
}

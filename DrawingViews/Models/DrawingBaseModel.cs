namespace Maporizer.DrawingViews.Models;

public abstract class DrawingBaseModel : IDrawable
{
    public Point Location { get; set; }
    public Size Size { get; set; }
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public DrawingBaseModel()
    {
        Location = new Point();
        Size = new Size();
        StrokeColor = Colors.Transparent;
        FillColor = Colors.Transparent;
    }
    public abstract void Draw(ICanvas canvas, RectF dirtyRect);
    public abstract bool IsCollidedWith(Point point);
    public abstract bool IsCollidedWith(PointF point);
}

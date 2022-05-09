namespace Maporizer.DrawingViews.Models;

public interface IDrawable
{
    public PointF Location { get; set; }
    public SizeF Size { get; set; }
    public Color StrokeColor { get; set; }
    public Color FillColor { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect);
    public bool IsCollidedWith(Point point);
    public bool IsCollidedWith(PointF point);
}

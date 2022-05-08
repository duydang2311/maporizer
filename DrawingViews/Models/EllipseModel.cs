namespace Maporizer.DrawingViews.Models;

public class EllipseModel : DrawingBaseModel
{
    public EllipseModel() : base() { }
    public override void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = StrokeColor;
        canvas.FillColor = FillColor;
        canvas.DrawEllipse(new Rect(Location, Size));
    }
}

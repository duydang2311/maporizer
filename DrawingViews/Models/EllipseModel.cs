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
}

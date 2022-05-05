using Avalonia;
using Avalonia.Media;

namespace Maporizer.CanvasViews.Models.Drawings;

public class EllipseModel : DrawingBaseModel
{
    public override void Draw(DrawingContext ctx)
    {
        var radiusX = Size.Width / 2;
        var radiusY = Size.Height / 2;
        ctx.DrawEllipse(Brush, Pen, new Point(Location.X + radiusX, Location.Y + radiusY), radiusX, radiusY);
    }
}
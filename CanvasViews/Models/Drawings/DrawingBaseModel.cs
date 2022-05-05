using Avalonia;
using Avalonia.Media;

namespace Maporizer.CanvasViews.Models.Drawings;

public abstract class DrawingBaseModel : IDrawable
{
    public Size Size { get; set; }
    public Point Location { get; set; }
    public IBrush? Brush { get; set; }
    public IPen? Pen { get; set; }

    public abstract void Draw(DrawingContext ctx);
}
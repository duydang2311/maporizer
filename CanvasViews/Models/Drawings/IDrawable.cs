using Avalonia.Media;

namespace Maporizer.CanvasViews.Models.Drawings;

public interface IDrawable
{
    public void Draw(DrawingContext ctx);
}
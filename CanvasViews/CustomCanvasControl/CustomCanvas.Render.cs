using Avalonia.Controls;
using Avalonia.Media;

namespace Maporizer.CanvasViews.CustomCanvasControl;

public partial class CustomCanvas : Canvas
{
    public override void Render(DrawingContext context)
    {
        base.Render(context);
        lock (DrawingFactory)
        {
            foreach (var drawing in DrawingFactory)
            {
                drawing.Draw(context);
            }
        }
    }
}
using System.Collections.Generic;
using Avalonia.Controls;
using Maporizer.CanvasViews.Models.Drawings;

namespace Maporizer.CanvasViews.CustomCanvasControl;

public partial class CustomCanvas : Canvas
{
    public readonly LinkedList<IDrawable> DrawingFactory = new();

    public void Draw(IDrawable drawing)
    {
        lock (DrawingFactory)
        {
            DrawingFactory.AddLast(drawing);
        }
    }
}
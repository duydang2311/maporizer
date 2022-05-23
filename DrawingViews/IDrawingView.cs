using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews;

public interface IDrawingView : IContentView
{
    public IGraphicsView GraphicsView { get; }
    public DrawingMode Mode { get; }
}

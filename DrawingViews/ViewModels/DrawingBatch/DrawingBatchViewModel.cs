using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    public IDrawingView Root { get; }
    public GraphicsView GraphicsView { get; }
    public PolygonModel? PolygonBatch { get; private set; } = null;
    public DrawingBatchViewModel(IDrawingView root)
    {
        Root = root;
        GraphicsView = (GraphicsView)root.GraphicsView;
        InitDrawInternal();
        InitEraseInternal();
    }
}

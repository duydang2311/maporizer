using Maporizer.DrawingViews.Models;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    public GraphicsView View { get; }
    public PolygonModel? PolygonBatch { get; private set; } = null;
    public DrawingBatchViewModel(GraphicsView graphicsView)
    {
        View = graphicsView;
        InitDrawInternal();
    }
}

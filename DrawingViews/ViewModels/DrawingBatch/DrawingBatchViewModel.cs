using Maporizer.DrawingViews.Models;
using Maporizer.DrawingToolBarViews.Models;
using Maporizer.DrawingToolBarViews.ViewModels;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    public GraphicsView GraphicsView { get; }
    public PolygonModel? PolygonBatch { get; private set; } = null;
    public DrawingBatchViewModel(GraphicsView graphicsView)
    {
        GraphicsView = graphicsView;
        InitMoveInternal();
        InitDrawInternal();
        InitEraseInternal();
        InitDrawingModeInternal();
    }
}

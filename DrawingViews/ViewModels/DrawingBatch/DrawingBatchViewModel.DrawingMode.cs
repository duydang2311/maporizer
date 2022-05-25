using Maporizer.DrawingViews.Models;
using Maporizer.DrawingToolBarViews.Models;
using Maporizer.DrawingToolBarViews.ViewModels;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    public DrawingMode Mode { get; private set; }
    private void InitDrawingModeInternal()
    {
        Mode = DrawingMode.None;
        MessagingCenter.Subscribe<DrawingToolBarViewModel, DrawingMode>(this, "DrawingModeChanged", DrawingModeChanged);
    }
    private void DrawingModeChanged(DrawingToolBarViewModel sender, DrawingMode mode)
    {
        Mode = mode;
    }
}

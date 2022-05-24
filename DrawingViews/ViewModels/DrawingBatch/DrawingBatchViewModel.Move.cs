using Maporizer.DrawingToolBarViews.Models;
using Maporizer.DrawingViews.Models;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class DrawingBatchViewModel
{
    private IDrawableShape? movingDrawing = null;
    private PointF movingLastPoint;
    private void InitMoveInternal()
    {
        GraphicsView.StartInteraction += Move_View_StartInteraction;
        GraphicsView.MoveHoverInteraction += Move_View_MoveHoverInteraction;
        GraphicsView.EndInteraction += Move_View_EndInteraction;
    }
    private void Move_View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (Root.Mode != DrawingMode.Move)
        {
            return;
        }
        if (movingDrawing is null)
        {
            var drawing = ((IDrawable)GraphicsView.Drawable).HoveringDrawing;
            if (drawing is not null)
            {
                movingDrawing = drawing;
                movingLastPoint = e.Touches[0];
            }
        }
    }
    private void Move_View_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        if (Root.Mode != DrawingMode.Move || movingDrawing is null)
        {
            return;
        }
        movingDrawing.Translate(e.Touches[0] - movingLastPoint);
        movingLastPoint = e.Touches[0];
        GraphicsView.Invalidate();
    }
    private void Move_View_EndInteraction(object? sender, TouchEventArgs e)
    {
        movingDrawing = null;
    }
}

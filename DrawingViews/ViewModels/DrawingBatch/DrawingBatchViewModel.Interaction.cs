using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    private bool interacting;
    private PointF lastPoint;
    public void InitInteractionInternal()
    {
        interacting = false;
        View.StartInteraction += View_StartInteraction;
        View.MoveHoverInteraction += View_MoveHoverInteraction;
        View.EndInteraction += View_EndInteraction;
    }
    private void View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (PolygonBatch is null)
        {
            interacting = true;
            Task.Run(() =>
            {
                Thread.Sleep(50);
                if (!interacting)
                {
                    return;
                }
                PolygonBatch = new PolygonModel { StrokeColor = Colors.White };
                PolygonBatch.Scale(((GraphicsDrawableModel)View.Drawable).ScaleFactor);
                ((GraphicsDrawableModel)View.Drawable).Draw(PolygonBatch);
            });
        }
    }
    private void View_EndInteraction(object? sender, TouchEventArgs e)
    {
        if (interacting)
        {
            interacting = false;
            if (PolygonBatch is not null)
            {
                PolygonBatch.Simplify();
                PolygonBatch.Close();
                PolygonBatch = null;
                lastPoint = Point.Zero;
                View.Invalidate();
            }
        }
    }
    private void View_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        if (PolygonBatch is not null)
        {
            var point = e.Touches[0];
            if (lastPoint.Distance(point) >= 2)
            {
                PolygonBatch.Add(point);
                lastPoint = point;
                View.Invalidate();
            }
        }
    }
}

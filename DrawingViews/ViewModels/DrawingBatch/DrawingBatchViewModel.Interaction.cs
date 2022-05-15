using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.Helpers;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    private bool drawing;
    private PointF lastPoint;
    public void InitInteractionInternal()
    {
        drawing = false;
        View.StartInteraction += View_StartInteraction;
        View.MoveHoverInteraction += View_MoveHoverInteraction;
        View.EndInteraction += View_EndInteraction;
    }
    private void View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (!drawing)
        {
            drawing = true;
        }
    }
    private void View_EndInteraction(object? sender, TouchEventArgs e)
    {
        if (drawing)
        {
            drawing = false;
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
        if (drawing)
        {
            if (PolygonBatch is null)
            {
                PolygonBatch = new PolygonModel { StrokeColor = Colors.White };
                PolygonBatch.Scale(((GraphicsDrawableModel)View.Drawable).ScaleFactor);
                ((GraphicsDrawableModel)View.Drawable).Draw(PolygonBatch);
            }
            var point = e.Touches[0];
            if (PointHelper.DistanceSquared(lastPoint, point) > 9)
            {
                PolygonBatch.Add(point);
                lastPoint = point;
                View.Invalidate();
            }
        }
    }
}

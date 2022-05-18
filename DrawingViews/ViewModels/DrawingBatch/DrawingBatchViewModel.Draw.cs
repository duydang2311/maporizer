using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.Helpers;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    private bool drawing;
    private PointF? lastPoint = null;
    public void InitDrawInternal()
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
            var drawable = (GraphicsDrawableModel)View.Drawable;
            if (drawable.HoveringDrawing is not null && drawable.HoveringDrawing is PolygonModel polygon)
            {
                var intersect = polygon.GetIntersectionPoint(e.Touches[0]);
                if (intersect is not null)
                {
                    lastPoint = intersect.Value;
                }
            }
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
                PolygonBatch.Simplify(45f * (View.Drawable as GraphicsDrawableModel)!.ScaleFactor);
                PolygonBatch.Close();
                PolygonBatch = null;
                lastPoint = null;
                View.Invalidate();
            }
        }
    }
    private void View_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        if (drawing)
        {
            var drawable = (GraphicsDrawableModel)View.Drawable;
            if (PolygonBatch is null)
            {
                PolygonBatch = new PolygonModel { StrokeColor = Colors.White };
                PolygonBatch.Scale(drawable.ScaleFactor);
                drawable.Draw(PolygonBatch);
                if (lastPoint is not null)
                {
                    PolygonBatch.Add(lastPoint.Value);
                }
            }
            var point = e.Touches[0];
            if (lastPoint is null || GeometryHelper.DistanceSquared(lastPoint.Value, point) > 16 * drawable.ScaleFactor)
            {
                PolygonBatch.Add(point);
                lastPoint = point;
                View.Invalidate();
            }
        }
    }
}

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
                PolygonBatch.Simplify(45f * ((GraphicsDrawableModel)View.Drawable).ScaleFactor);
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
            var drawable = (GraphicsDrawableModel)View.Drawable;
            if (PolygonBatch is null)
            {
                PolygonBatch = new PolygonModel { StrokeColor = Colors.White };
                PolygonBatch.Scale(drawable.ScaleFactor);
                drawable.Draw(PolygonBatch);
            }
            var point = e.Touches[0];
            if (GeometryHelper.DistanceSquared(lastPoint, point) > 16 * drawable.ScaleFactor)
            {
                PolygonBatch.Add(point);
                lastPoint = point;
                View.Invalidate();
            }
        }
    }
}

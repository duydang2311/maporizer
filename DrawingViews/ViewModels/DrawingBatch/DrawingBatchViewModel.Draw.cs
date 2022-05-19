using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.Helpers;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

using Maporizer.DrawingViews.Models;

public partial class DrawingBatchViewModel
{
    private bool drawing = false;
    private PointF lastPoint;
    private IDrawable? clippingDrawable = null;
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
                var intersect = polygon.GetIntersectionPoint(e.Touches[0], (float)Math.Pow(polygon.StrokeWidth, 4));
                if (intersect is null)
                {
                    return;
                }
                lastPoint = intersect.Value;
                clippingDrawable = polygon;
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
                if (clippingDrawable is not null)
                {
                    PolygonBatch.Clip((PolygonModel)clippingDrawable);
                }
                PolygonBatch.Close();
                PolygonBatch = null;
                lastPoint = PointF.Zero;
                clippingDrawable = null;
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
                if (clippingDrawable is not null)
                {
                    PolygonBatch.Add(lastPoint);
                }
            }
            var point = e.Touches[0];
            if (GeometryHelper.DistanceSquared(lastPoint, point) > 16 * drawable.ScaleFactor)
            {
                if (clippingDrawable is not null)
                {
                    if(!clippingDrawable.HasPointIn(point))
                    {
                        return;
                    }
                    var intersect = clippingDrawable.GetIntersectionPoint(point, (float)Math.Pow((clippingDrawable as PolygonModel)!.StrokeWidth, 3));
                    if (intersect is not null)
                    {
                        point = intersect.Value;
                    }
                }
                PolygonBatch.Add(point);
                lastPoint = point;
                View.Invalidate();
            }
        }
    }
}

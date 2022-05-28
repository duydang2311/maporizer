using Maporizer.DrawingToolBarViews.Models;
using Maporizer.Helpers;
using Maporizer.DrawingViews.Models;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class DrawingBatchViewModel
{
    private bool drawing = false;
    private PointF lastPoint;
    private IDrawableShape? clippingDrawable = null;
    private void InitDrawInternal()
    {
        drawing = false;
        GraphicsView.StartInteraction += Draw_View_StartInteraction;
        GraphicsView.MoveHoverInteraction += Draw_View_MoveHoverInteraction;
        GraphicsView.EndInteraction += Draw_View_EndInteraction;
    }
    private void Draw_View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (drawing || Mode != DrawingMode.Draw)
        {
            return;
        }
        var drawable = (GraphicsDrawableModel)GraphicsView.Drawable;
        if (drawable.HoveringDrawing is not null && drawable.HoveringDrawing is PolygonModel polygon)
        {
            var intersect = polygon.GetIntersectionPoint(e.Touches[0], (float)Math.Pow(polygon.StrokeWidth, 4));
            if (intersect is null)
            {
                return;
            }
            lastPoint = intersect.Value;
            clippingDrawable = polygon;
            polygon.Ignored = true;
        }
        drawing = true;
    }
    private void Draw_View_EndInteraction(object? sender, TouchEventArgs e)
    {
        if (drawing)
        {
            drawing = false;
            if (PolygonBatch is not null)
            {
                lastPoint = PointF.Zero;
                PolygonBatch.Simplify(45f * (GraphicsView.Drawable as GraphicsDrawableModel)!.ScaleFactor);
                if (clippingDrawable is not null)
                {
                    var success = PolygonBatch.TryClip((PolygonModel)clippingDrawable);
                    clippingDrawable!.Ignored = false;
                    clippingDrawable = null;

                    if (!success)
                    {
                        (GraphicsView.Drawable as IGraphicsDrawable)!.Erase(PolygonBatch);
                        PolygonBatch.Path.Dispose();
                        PolygonBatch = null;
                        GraphicsView.Invalidate();
                        return;
                    }
                }
                PolygonBatch.Close();
                PolygonBatch = null;
                GraphicsView.Invalidate();
            }
        }
    }
    private void Draw_View_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        if (drawing)
        {
            var drawable = (GraphicsDrawableModel)GraphicsView.Drawable;
            if (PolygonBatch is null)
            {
                PolygonBatch = new PolygonModel { StrokeColor = ThemeHelper.GetThemeBasedValue((Color)App.Current!.Resources["Black"], (Color)App.Current!.Resources["White"]) };
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
                        // bad practice but ehh
                        Draw_View_EndInteraction(null, new TouchEventArgs());
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
                GraphicsView.Invalidate();
            }
        }
    }
}

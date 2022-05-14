using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    private bool interacting;
    private bool disposed;
    private PointF lastPoint;
    private CancellationTokenSource tokenSource;
    private object mutex = new ();
    public void InitInteractionInternal()
    {
        interacting = false;
        disposed = true;
        View.StartInteraction += View_StartInteraction;
        View.MoveHoverInteraction += View_MoveHoverInteraction;
        View.EndInteraction += View_EndInteraction;
    }
    private int i = 0;
    private void View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (disposed)
        {
            interacting = true;
            lock (mutex)
            {
                if (!disposed && tokenSource is not null)
                {
                    tokenSource.Cancel();
                    tokenSource.Dispose();
                }
                tokenSource = new CancellationTokenSource();
                disposed = false;
            }
            var waitHandle = tokenSource.Token.WaitHandle;
            var task = Task.Run(() =>
            {
                bool cancelled = false;
                lock (mutex)
                {
                    cancelled = waitHandle.WaitOne(100);
                    tokenSource.Dispose();
                    disposed = true;
                }
                if (cancelled || !interacting)
                {
                    return;
                }
                App.Current.Dispatcher.Dispatch(() =>
                {
                    if (interacting)
                    {
                        PolygonBatch = new PolygonModel { StrokeColor = Colors.White };
                        PolygonBatch.Scale(((GraphicsDrawableModel)View.Drawable).ScaleFactor);
                        ((GraphicsDrawableModel)View.Drawable).Draw(PolygonBatch);
                    }
                });
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
            lock (mutex)
            {
                if (!disposed)
                {
                    tokenSource.Cancel();
                }
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

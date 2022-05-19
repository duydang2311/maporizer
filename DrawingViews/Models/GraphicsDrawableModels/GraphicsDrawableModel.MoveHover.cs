using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : Microsoft.Maui.Graphics.IDrawable
{
    private Thread moveHover_thread;
    private AutoResetEvent moveHover_resetEvent;
    private Point moveHover_touchPoint;
    private IDrawable? moveHover_drawing;
    private readonly object mutex = new();
    public IDrawable? HoveringDrawing { get => moveHover_drawing; }
    private void InitMoveHoverInternal()
    {
        moveHover_resetEvent = new AutoResetEvent(false);
        GraphicsView.MoveHoverInteraction += GraphicsView_MoveHoverInteraction;
        moveHover_thread = new Thread(Thread_MoveHoverLoop) { IsBackground = true };
        moveHover_thread.Start();
    }
    private void Thread_MoveHoverLoop()
    {
        while (true)
        {
            moveHover_resetEvent.WaitOne();
            lock (mutex)
            lock (Drawings)
            {
                IDrawable? collided = null;
                foreach (IDrawable drawing in Drawings)
                {
                    if (drawing.IsCollidedWith(moveHover_touchPoint, true))
                    {
                        collided = drawing;
                        break;
                    }
                }
                if (collided != moveHover_drawing)
                {
                    var dispatcher = App.Current!.Dispatcher;
                    var reset = moveHover_drawing;
                    moveHover_drawing = collided;
                    dispatcher.Dispatch(() =>
                    {
                        if (reset is not null)
                        {
                            reset.FillColor = Colors.Transparent;
                        }
                        if (collided is not null)
                        {
                            collided.FillColor = Colors.ForestGreen;
                        }
                        GraphicsView.Invalidate();
                    });
                }
            }
        }
    }
    private void GraphicsView_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        lock(mutex)
        {
            moveHover_touchPoint = e.Touches[0];
        }
        moveHover_resetEvent.Set();
    }
}


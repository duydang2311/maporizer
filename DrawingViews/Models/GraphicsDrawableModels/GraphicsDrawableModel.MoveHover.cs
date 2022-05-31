using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IGraphicsDrawable
{
    private Thread moveHover_thread;
    private AutoResetEvent moveHover_resetEvent;
    private Point moveHover_touchPoint;
    private IDrawableShape? moveHover_drawing;
    private readonly object mutex = new();
    private bool running = true;
    public IDrawableShape? HoveringDrawing { get => moveHover_drawing; }
    public void PauseHovering()
    {
        running = false;
    }
    public void ResumeHovering()
    {
        running = true;
    }
    private void InitMoveHoverInternal()
    {
        moveHover_resetEvent = new AutoResetEvent(false);
        GraphicsView.MoveHoverInteraction += MoveHoverInteractionInternal;
        moveHover_thread = new Thread(HandleMoveHoverLoopInternal) { IsBackground = true };
        moveHover_thread.Start();
    }
    private void HandleMoveHoverLoopInternal()
    {
        while (true)
        {
            if (!running)
            {
                moveHover_drawing = null;
                continue;
            }
            moveHover_resetEvent.WaitOne();
            lock (mutex)
            lock (Drawings)
            {
                IDrawableShape? collided = null;
                var last = Drawings.Last;
                while(last is not null)
                {
                    if (!last.Value.Ignored && last.Value.HasPointIn(moveHover_touchPoint))
                    {
                        collided = last.Value;
                        break;
                    }
                    last = last.Previous;
                }
                if (collided != moveHover_drawing)
                {
                    var reset = moveHover_drawing;
                    moveHover_drawing = collided;
                    GraphicsView.Dispatcher.Dispatch(() =>
                    {
                        if (reset is not null)
                        {
                            reset.FillColor = Colors.Transparent;
                        }
                        if (collided is not null)
                        {
                            collided.FillColor = ThemeHelper.GetThemeBasedValue((Color)App.Current!.Resources["Primary"], (Color)App.Current.Resources["Secondary"]);
                        }
                        GraphicsView.Invalidate();
                    });
                }
            }
        }
    }
    private void MoveHoverInteractionInternal(object? sender, TouchEventArgs e)
    {
        lock(mutex)
        {
            moveHover_touchPoint = e.Touches[0];
        }
        moveHover_resetEvent.Set();
    }
}


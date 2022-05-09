﻿namespace Maporizer.DrawingViews.ViewModels;

using Maporizer.DrawingViews.Models;

public partial class GraphicsDrawableViewModel : Microsoft.Maui.Graphics.IDrawable
{
    private Thread moveHover_thread;
    private AutoResetEvent moveHover_resetEvent;
    private Point moveHover_touchPoint;
    private IDrawable? moveHover_drawing;
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
            lock (Drawings)
            {
                IDrawable? collided = null;
                foreach (var drawing in Drawings)
                {
                    if (drawing.IsCollidedWith(moveHover_touchPoint))
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
                    lock (dispatcher)
                    {
                        dispatcher.Dispatch(() =>
                        {
                            if (reset is not null)
                            {
                                reset.StrokeColor = Colors.White;
                            }
                            if (collided is not null)
                            {
                                collided.StrokeColor = Colors.Red;
                            }
                            GraphicsView.Invalidate();
                        });
                    }
                }
            }
        }
    }
    private void GraphicsView_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        moveHover_touchPoint = e.Touches[0];
        moveHover_resetEvent.Set();
    }
}


namespace Maporizer.DrawingViews.ViewModels;

using Maporizer.DrawingViews.Models;

public partial class GraphicsDrawableViewModel : Microsoft.Maui.Graphics.IDrawable
{
    private const float defaultSize = 100f;
    public LinkedList<IDrawable> Drawings { get; }
    public GraphicsView GraphicsView { get; }
    public GraphicsDrawableViewModel(GraphicsView graphicsView)
    {
        Drawings = new();
        GraphicsView = graphicsView;
        GraphicsView.StartInteraction += GraphicsView_StartInteraction;

        moveHover_thread = null!;
        moveHover_resetEvent = null!;
        InitMoveHoverInternal();
    }
    public void Draw(IDrawable drawing)
    {
        lock (Drawings)
        {
            Drawings.AddLast(drawing);
        }
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        lock (Drawings)
        {
            canvas.Antialias = true;
            foreach (var drawing in Drawings)
            {
                drawing.Draw(canvas, dirtyRect);
            }
        }
    }
    private void GraphicsView_StartInteraction(object? sender, TouchEventArgs e)
    {
        Draw(new EllipseModel
        {
            Location = e.Touches[0].Offset(-defaultSize / 2, -defaultSize / 2),
            Size = new Size(defaultSize),
            FillColor = (Color)Application.Current!.Resources["Primary"],
            StrokeColor = (Color)Application.Current!.Resources["Secondary"]
        });
        GraphicsView.Invalidate();
    }
}


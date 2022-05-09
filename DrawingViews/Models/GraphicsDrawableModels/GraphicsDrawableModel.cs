namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : Microsoft.Maui.Graphics.IDrawable
{
    private const float defaultSize = 100f;
    public LinkedList<IDrawable> Drawings { get; }
    public GraphicsView GraphicsView { get; }
    private float scaleFactor = 1f;
    public float ScaleFactor {
        get => scaleFactor;
        set
        {
            lock (Drawings)
            {
                PointF location;
                SizeF size;
                float invert = value / scaleFactor;
                foreach (var drawing in Drawings)
                {
                    location = drawing.Location;
                    size = drawing.Size;
                    drawing.Location = new PointF(location.X * invert, location.Y * invert);
                    drawing.Size = new SizeF(size.Width * invert, size.Height * invert);
                }
            }
            scaleFactor = value;
        }
    }
    public GraphicsDrawableModel(GraphicsView graphicsView)
    {
        Drawings = new();
        scaleFactor = 1f;
        GraphicsView = graphicsView;
        GraphicsView.StartInteraction += GraphicsView_StartInteraction;

        moveHover_thread = null!;
        moveHover_resetEvent = null!;
        InitMoveHoverInternal();
    }
}


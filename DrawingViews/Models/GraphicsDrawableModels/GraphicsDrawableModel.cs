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
                float invert = value / scaleFactor;
                foreach (var drawing in Drawings)
                {
                    drawing.Scale(invert);
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

        moveHover_thread = null!;
        moveHover_resetEvent = null!;
        InitMoveHoverInternal();
    }
}


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
        GraphicsView.StartInteraction += GraphicsView_StartInteraction;

        moveHover_thread = null!;
        moveHover_resetEvent = null!;
        InitMoveHoverInternal();

        var polygon = new PolygonModel { StrokeColor = Colors.Red, FillColor = Colors.Green };
        App.Current!.Dispatcher.DispatchDelayed(new TimeSpan(0, 0, 0, 0, 500), () =>
        {
            polygon.Add(new PointF(300, 300));
            GraphicsView.Invalidate();
        });
        App.Current!.Dispatcher.DispatchDelayed(new TimeSpan(0, 0, 0, 0, 1000), () =>
        {
            polygon.Add(new PointF(0, 300));
            GraphicsView.Invalidate();
        });
        App.Current!.Dispatcher.DispatchDelayed(new TimeSpan(0, 0, 0, 0, 1500), () =>
        {
            polygon.Add(new PointF(0, 600));
            GraphicsView.Invalidate();
        });
        App.Current!.Dispatcher.DispatchDelayed(new TimeSpan(0, 0, 0, 0, 2000), () =>
        {
            polygon.Add(new PointF(600, 600));
            polygon.Close();
            GraphicsView.Invalidate();
        });
        Drawings.AddLast(polygon);
    }
}


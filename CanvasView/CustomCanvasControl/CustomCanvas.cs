using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;

namespace Maporizer.CanvasView.CustomCanvasControl;

internal class CustomCanvas : Canvas
{
    private Compositor _compositor;
    private ContainerVisual _root;
    private CompositionContainerShape _containerShape;
    private ShapeVisual _shapeVisual;
    public CustomCanvas() : base()
    {
        _root = ElementCompositionPreview.GetElementVisual(this).Compositor.CreateContainerVisual();
        ElementCompositionPreview.SetElementChildVisual(this, _root);
        _compositor = _root.Compositor;
        _containerShape = _compositor.CreateContainerShape();
        _shapeVisual = _compositor.CreateShapeVisual();
        _shapeVisual.Size = new Vector2(10000f);
        _shapeVisual.Shapes.Add(_containerShape);

        _root.Children.InsertAtTop(_shapeVisual);
        Draw();
    }
    private void DrawEllipse(Vector2 size, Vector2 location)
    {
        var ellipse = _compositor.CreateEllipseGeometry();
        ellipse.Radius = size;

        var shape = _compositor.CreateSpriteShape(ellipse);
        _compositor.CreateSpriteShape();
        shape.FillBrush = _compositor.CreateColorBrush(Colors.ForestGreen);
        shape.Offset = size + location;

        _containerShape.Shapes.Add(shape);
    }
    public void Draw()
    {
        for (int i = 0; i != 500; ++i)
        {
            DrawEllipse(new Vector2(50f), new Vector2(i * 50f, i * 50f));
        }
        AutoResetEvent drawEvent = new(false);
        Task.Run(() =>
        {
            while (DispatcherQueue is not null)
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    lock (_containerShape.Shapes)
                    {
                        foreach (var i in _containerShape.Shapes)
                        {
                            i.Offset = new Vector2(i.Offset.X + 2, i.Offset.Y);
                        }
                        drawEvent.Set();
                    }
                });
                drawEvent.WaitOne();
                Thread.Sleep(15);
            }
        });
    }
}

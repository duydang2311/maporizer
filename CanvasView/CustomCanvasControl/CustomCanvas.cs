using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System.Numerics;

namespace Maporizer.CanvasView.CustomCanvasControl;

internal class CustomCanvas : Canvas
{
    private Compositor _compositor;
    private ContainerVisual _root;
    public CustomCanvas() : base()
    {
        _root = ElementCompositionPreview.GetElementVisual(this).Compositor.CreateContainerVisual();
        ElementCompositionPreview.SetElementChildVisual(this, _root);
        _compositor = _root.Compositor;
        Draw();
    }
    public void Draw()
    {
        var container = _compositor.CreateContainerShape();

        var ellipse = _compositor.CreateEllipseGeometry();
        ellipse.Radius = new Vector2(300f, 300f);

        var rect = _compositor.CreateRoundedRectangleGeometry();
        rect.Size = new Vector2(600f, 600f);
        rect.CornerRadius = new Vector2(300f);

        var shape = _compositor.CreateSpriteShape(ellipse);
        shape.FillBrush = _compositor.CreateColorBrush(Colors.ForestGreen);
        shape.Offset = new Vector2(500f, 300f);
        container.Shapes.Add(shape);

        shape = _compositor.CreateSpriteShape(rect);
        shape.FillBrush = _compositor.CreateColorBrush(Colors.Silver);
        shape.Offset = new Vector2(900f, 0);
        shape.CenterPoint = new Vector2(0, 0);
        container.Shapes.Add(shape);

        var visual = _compositor.CreateShapeVisual();
        visual.Size = new Vector2(2000f, 2000f);
        visual.Shapes.Add(container);
        _root.Offset = new Vector3(0, 0, 0);
        _root.Children.InsertAtTop(visual);
    }
}

using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Maporizer.CanvasViews.Models.Drawings;

namespace Maporizer.CanvasViews.CustomCanvasControl;

public partial class CustomCanvas : Canvas
{
    private Label text;
    public CustomCanvas()
    {
        PointerMoved += _OnPointerMoved;
        text = new Label {Content = "OK"};
        Children.Add(text);
        // for (int i = 0; i != 1000; ++i)
        // {
        //     Draw(new EllipseModel {Brush = Brushes.ForestGreen, Location = new Point(10 + (i / 50) * 10, (i % 50) * 10), Size = new Size(10, 10)});
        // }
        // Task.Run(async () =>
        // {
        //     while (true)
        //     {
        //         lock (DrawingFactory)
        //         {
        //             foreach (var drawing in DrawingFactory)
        //             {
        //                 if (drawing is not EllipseModel ellipse) continue;
        //                 ellipse.Location = new Point((ellipse.Location.X + 10) >= 1000 ? ellipse.Location.X - 990 : ellipse.Location.X + 10, ellipse.Location.Y);
        //             }
        //         }
        //         InvalidateVisual();
        //         await Task.Delay(10);
        //     }
        // });
    }
}
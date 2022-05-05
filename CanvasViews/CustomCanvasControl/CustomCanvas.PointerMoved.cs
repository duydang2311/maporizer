using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Maporizer.CanvasViews.Models.Drawings;

namespace Maporizer.CanvasViews.CustomCanvasControl;

public partial class CustomCanvas : Canvas
{
    private void _OnPointerMoved(object? args, PointerEventArgs e)
    {
        text.Content = e.GetPosition(this).ToString() + " " + e.GetCurrentPoint(this).Position.ToString();
        // InvalidateVisual();
    }
}
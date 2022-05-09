using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
    private const float scaleUnit = 25f;

    private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        int remainder = (int)Math.Round(e.NewValue / scaleUnit);
        var snappedValue = remainder * 25;
        (sender as Slider)!.Value = snappedValue;
        ((GraphicsDrawableModel)GraphicsView.Drawable).ScaleFactor = snappedValue / 100f;
        GraphicsView.Invalidate();
    }
}

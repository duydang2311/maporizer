using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
    private const float scaleUnit = 25f;

    private void Slider_Scaling_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        var drawable = (GraphicsDrawableModel)_GraphicsView.Drawable;
        int remainder = (int)Math.Round(e.NewValue / scaleUnit);
        var snappedValue = remainder * 25;
        var oldScaleFactor = drawable.ScaleFactor;
        (sender as Slider)!.Value = snappedValue;
        _GraphicsView.WidthRequest = _GraphicsView.WidthRequest / oldScaleFactor * (snappedValue / 100f);
        _GraphicsView.HeightRequest = _GraphicsView.HeightRequest / oldScaleFactor * (snappedValue / 100f);
        drawable.ScaleFactor = snappedValue / 100f;
        _GraphicsView.Invalidate();
    }
}

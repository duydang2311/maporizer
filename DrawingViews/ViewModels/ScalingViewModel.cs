using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
    private const float scaleUnit = 25f;

    private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        var drawable = (GraphicsDrawableModel)GraphicsView.Drawable;
        int remainder = (int)Math.Round(e.NewValue / scaleUnit);
        var snappedValue = remainder * 25;
        var oldScaleFactor = drawable.ScaleFactor;
        (sender as Slider)!.Value = snappedValue;
        GraphicsView.WidthRequest = GraphicsView.WidthRequest / oldScaleFactor * (snappedValue / 100f);
        GraphicsView.HeightRequest = GraphicsView.HeightRequest / oldScaleFactor * (snappedValue / 100f);
        drawable.ScaleFactor = snappedValue / 100f;
        GraphicsView.Invalidate();
    }
}

using Maporizer.Helpers;

namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : IDrawable
{
    private void InitThemeInternal()
    {
        App.Current!.RequestedThemeChanged += App_RequestedThemeChanged;
    }
    private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        lock (Drawings)
        {
            var stroke = ThemeHelper.GetThemeBasedValue((Color)App.Current!.Resources["Black"], (Color)App.Current!.Resources["White"]);
            foreach (var i in Drawings)
            {
                i.StrokeColor = stroke;
            }
        }
    }
}


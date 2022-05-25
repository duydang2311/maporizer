namespace Maporizer.Helpers;

public static class ThemeHelper
{
    public static ImageSource GetImageSource(string path)
    {
        return ImageSource.FromFile
        (
            App.Current!.RequestedTheme != AppTheme.Dark
            ? path + "_light.png"
            : path + "_dark.png"
        );
    }
    public static T GetThemeBasedValue<T>(T light, T dark)
    {
        return App.Current!.RequestedTheme != AppTheme.Light
            ? light
            : dark;
    }
}

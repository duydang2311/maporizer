namespace Maporizer.Helpers;

public static class ThemeHelper
{
    public static ImageSource GetImageSource(string path)
    {
        return ImageSource.FromFile
        (
            App.Current!.PlatformAppTheme != AppTheme.Dark
            ? path + "_light.png"
            : path + "_dark.png"
        );
    }
}

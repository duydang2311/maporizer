using _FileSavePicker = Windows.Storage.Pickers.FileSavePicker;
using Windows.Storage.Pickers;
using Windows.Storage;
using Maporizer.FileSavePickers;

namespace Maporizer.Platforms.Windows;

public class FileSavePicker : IFileSavePicker
{
    public async Task<string?> PickAsync()
    {
        var savePicker = new _FileSavePicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            FileTypeChoices = { { "Maporizer Map", new string[] { ".mapo" } } },
            SuggestedFileName = "Map"
        };

        var hwnd = ((MauiWinUIWindow)App.Current!.Windows[0].Handler.PlatformView!).WindowHandle;

        WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
        var result = await savePicker.PickSaveFileAsync();
        return result?.Path;
    }
}

using Windows.Storage.Pickers;
using Maporizer.FileSavePickers;

namespace Maporizer.Platforms.Windows;

public class FileSavePicker : IFileSavePicker
{
    public async Task<string> PickAsync()
    {
        var savePicker = new FileSavePicker();
        savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
        savePicker.FileTypeChoices.Add("Maporizer Map", new List<string>() { ".mapo" });
        savePicker.SuggestedFileName = "Map";

        var hwnd = ((MauiWinUIWindow)App.Current!.Windows[0].Handler.PlatformView!).WindowHandle;

        WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
        var result = await savePicker.PickSaveFileAsync();
        return result.Path;
    }
}

using System.Windows.Input;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand ImportCommand { get; private set; } = null!;
    private void InitImportInternal()
    {
        ImportCommand = new Command<string?>(ImportCommandHandler);
    }
    private async void ImportCommandHandler(string? path = null)
    {
        if (path is not null)
        {
            MessagingCenter.Send(this, "Import", path);
            return;
        }
        var fileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new [] { ".mapo" } },
                { DevicePlatform.Android, new [] { ".mapo" } },
                { DevicePlatform.iOS, new [] { ".mapo" } },
                { DevicePlatform.macOS, new [] { ".mapo" } },
                { DevicePlatform.MacCatalyst, new [] { ".mapo" } },
            });
        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Select a map to import",
            FileTypes = fileType,
        });
        if (result is null)
        {
            return;
        }
        MessagingCenter.Send(this, "Import", result.FullPath);
    }
}

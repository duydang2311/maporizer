using System.Windows.Input;
using Maporizer.FileSavePickers;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand ImportCommand { get; private set; } = null!;
    private void InitImportInternal()
    {
        ImportCommand = new Command(ImportCommandHandler);
    }
    private async void ImportCommandHandler()
    {
        var fileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new [] { ".mapo" } },
                { DevicePlatform.Android, new [] { ".mapo" } },
                { DevicePlatform.iOS, new [] { ".mapo" } },
                { DevicePlatform.macOS, new [] { ".mapo" } },
                { DevicePlatform.MacCatalyst, new [] { ".mapo" } },
            });
        var path = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Select a map to import",
            FileTypes = fileType,
        });
        if (path is null)
        {
            return;
        }
        MessagingCenter.Send(this, "Import", path);
    }
}

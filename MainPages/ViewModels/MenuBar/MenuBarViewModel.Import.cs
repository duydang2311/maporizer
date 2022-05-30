using System.Windows.Input;
using Maporizer.FileSavePickers;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand ImportCommand { get; private set; } = null!;
    public ICommand ImportDefaultCommand { get; private set; } = null!;
    private void InitImportInternal()
    {
        ImportCommand = new Command(ImportCommandHandler);
        ImportDefaultCommand = new Command<string>(ImportDefaultCommandHandler);
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
    private async void ImportDefaultCommandHandler(string file)
    {
        var stream = await FileSystem.OpenAppPackageFileAsync(file);
        if (stream is null)
        {
            return;
        }
        var reader = new StreamReader(stream);
        MessagingCenter.Send(this, "Import", reader);
    }
}

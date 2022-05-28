using System.Windows.Input;
using Maporizer.FileSavePickers;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand ExportCommand { get; private set; } = null!;
    private void InitExportInternal()
    {
        ExportCommand = new Command(ExportCommandHandler);
    }
    private async void ExportCommandHandler()
    {
        var path = await MauiProgram.ServiceProvider.GetService<IFileSavePicker>()!.PickAsync();
        if (path is null)
        {
            return;
        }
        MessagingCenter.Send(this, "Export", path);
    }
}

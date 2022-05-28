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
        var path = await MauiProgram.ServiceProvider.GetService<IFileSavePicker>()!.PickAsync();
    }
}

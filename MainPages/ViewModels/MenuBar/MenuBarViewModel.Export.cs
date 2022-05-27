using System.Windows.Input;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand ExportCommand { get; private set; } = null!;
    private void InitExportInternal()
    {
        ExportCommand = new Command(ExportCommandHandler);
    }
    private void ExportCommandHandler()
    {
        System.Diagnostics.Debug.WriteLine("export");
        MessagingCenter.Send(this, "Export");
    }
}

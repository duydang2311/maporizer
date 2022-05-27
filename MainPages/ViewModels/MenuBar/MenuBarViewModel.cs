using Maporizer.FileSavePickers;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    private readonly IFileSavePicker fileSavePicker;
    public MenuBarViewModel()
    {
        fileSavePicker = null!;
        InitNewInternal();
        InitImportInternal();
        InitExportInternal();
    }
    public MenuBarViewModel(IFileSavePicker fileSavePicker)
    {
        this.fileSavePicker = fileSavePicker;
        InitNewInternal();
        InitImportInternal();
        InitExportInternal();
    }
}

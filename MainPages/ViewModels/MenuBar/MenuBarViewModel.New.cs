using System.Windows.Input;

namespace Maporizer.MainPages.ViewModels.MenuBar;

public partial class MenuBarViewModel
{
    public ICommand NewCommand { get; private set; } = null!;
    private void InitNewInternal()
    {
        NewCommand = new Command(NewCommandHandler);
    }
    private void NewCommandHandler()
    {
        MessagingCenter.Send(this, "New");
    }
}

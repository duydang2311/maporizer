using Maporizer.MainPages.ViewModels.Colorizer;
using Maporizer.MainPages.ViewModels.MenuBar;

namespace Maporizer.MainPages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
		_ = new ColorizerPromptViewModel(this);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Export", OnExport);
	}
	private void OnExport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Export", path);
    }
}


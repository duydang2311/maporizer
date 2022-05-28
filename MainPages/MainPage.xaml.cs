using Maporizer.MainPages.ViewModels.Colorizer;
using Maporizer.MainPages.ViewModels.MenuBar;
using Maporizer.DrawingViews;

namespace Maporizer.MainPages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
		_ = new ColorizerPromptViewModel(this);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Export", OnExport);
		MessagingCenter.Subscribe<DrawingView>(this, "Exported", OnExported);
	}
	private void OnExport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Export", path);
    }
	private async void OnExported(DrawingView sender)
    {
		await DisplayAlert("Export", "Your map has been exported successfully", "OK");
    }
}


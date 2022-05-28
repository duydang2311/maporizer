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
		MessagingCenter.Subscribe<MenuBarViewModel, FileResult>(this, "Import", OnImport);
		MessagingCenter.Subscribe<DrawingView, int>(this, "Exported", OnExported);
		MessagingCenter.Subscribe<DrawingView, int>(this, "Imported", OnImported);
	}
	private void OnExport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Export", path);
    }
	private async void OnExported(DrawingView sender, int drawings)
    {
		await DisplayAlert("Export", $"Your map with {drawings} drawings has been exported successfully", "OK");
    }
	private void OnImport(MenuBarViewModel sender, FileResult file)
    {
		MessagingCenter.Send(this, "Import", file);
    }
	private async void OnImported(DrawingView sender, int drawings)
    {
		await DisplayAlert("Export", $"Imported a map with {drawings} drawings", "OK");
    }
}


using Maporizer.MainPages.ViewModels.Colorizer;
using Maporizer.MainPages.ViewModels.MenuBar;
using Maporizer.DrawingViews;
using Maporizer.ColorizerPrompts.Models;

namespace Maporizer.MainPages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
		_ = new ColorizerPromptViewModel(this);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Export", OnExport);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Import", OnImport);
		MessagingCenter.Subscribe<DrawingView, int>(this, "Exported", OnExported);
		MessagingCenter.Subscribe<DrawingView, int>(this, "Imported", OnImported);
		MessagingCenter.Subscribe<ColorizerPromptViewModel, PromptResultModel>(this, "Colorize", OnColorize);
	}
	private void OnExport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Export", path);
    }
	private async void OnExported(DrawingView sender, int drawings)
    {
		await DisplayAlert("Export", $"Your map with {drawings} drawings has been exported successfully", "OK");
    }
	private void OnImport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Import", path);
    }
	private async void OnImported(DrawingView sender, int drawings)
    {
		await DisplayAlert("Export", $"Imported a map with {drawings} drawings", "OK");
    }
	private void OnColorize(ColorizerPromptViewModel sender, PromptResultModel model)
    {
        MessagingCenter.Send(this, "Colorize", model);
    }
}


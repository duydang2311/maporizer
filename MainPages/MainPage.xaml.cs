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
		MessagingCenter.Subscribe<MenuBarViewModel>(this, "New", OnNew);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Export", OnExport);
		MessagingCenter.Subscribe<MenuBarViewModel, string>(this, "Import", OnImport);
		MessagingCenter.Subscribe<DrawingView, string>(this, "Alert", OnAlert);
		MessagingCenter.Subscribe<ColorizerPromptViewModel, PromptResultModel>(this, "Colorize", OnColorize);
	}
	private void OnNew(MenuBarViewModel sender)
    {
		MessagingCenter.Send(this, "New");
    }
	private void OnExport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Export", path);
    }
	private async void OnAlert(DrawingView sender, string message)
    {
		await DisplayAlert("Export", message, "OK");
    }
	private void OnImport(MenuBarViewModel sender, string path)
    {
		MessagingCenter.Send(this, "Import", path);
    }
	private void OnColorize(ColorizerPromptViewModel sender, PromptResultModel model)
    {
        MessagingCenter.Send(this, "Colorize", model);
    }
}


using Maporizer.ViewModels;

namespace Maporizer.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
		MainMenuBarViewModel.InitMainMenuBar(this);
	}
}


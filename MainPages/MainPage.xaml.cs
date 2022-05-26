using CommunityToolkit.Maui.Views;
using Maporizer.ColorizerPopupViews;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.MainPages.ViewModels.Colorizer;

namespace Maporizer.MainPages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
		_ = new ColorizerPromptViewModel(this);
	}
}


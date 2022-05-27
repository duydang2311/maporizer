using CommunityToolkit.Maui.Views;

namespace Maporizer.ColorizerPrompts;

public partial class ColorizerPromptPopup : Popup
{
	public ColorizerPromptPopup()
	{
		InitializeComponent();
		InitOKInternal();
		InitCancelInternal();
	}
}
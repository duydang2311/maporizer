using CommunityToolkit.Maui.Views;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.ColorizerPopupViews;

namespace Maporizer.MainPages.ViewModels.Colorizer;

public partial class ColorizerPromptViewModel
{
    private void InitToolBarItemSelectedInternal()
    {
        MessagingCenter.Subscribe<DrawingToolBarViewModel, ToolBarItemViewModel>(page, "ItemSelected", OnItemSelected);
    }
    private void OnItemSelected(DrawingToolBarViewModel sender, ToolBarItemViewModel item)
    {
        if (item.Mode == DrawingToolBarViews.Models.DrawingMode.Colorize)
        {
            var popup = new ColorizerPromptPopup();
            page.ShowPopup(popup);
        }
    }
}

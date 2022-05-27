using CommunityToolkit.Maui.Views;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.ColorizerPrompts;
using Maporizer.ColorizerPrompts.Models;

namespace Maporizer.MainPages.ViewModels.Colorizer;

public partial class ColorizerPromptViewModel
{
    private void InitToolBarItemSelectedInternal()
    {
        MessagingCenter.Subscribe<DrawingToolBarViewModel, ToolBarItemViewModel>(page, "ItemSelected", OnItemSelected);
    }
    private async void OnItemSelected(DrawingToolBarViewModel sender, ToolBarItemViewModel item)
    {
        if (item.Mode == DrawingToolBarViews.Models.DrawingMode.Colorize)
        {
            var popup = new ColorizerPromptPopup();
            if (await page.ShowPopupAsync(popup) is PromptResultModel result)
            {
                // TODO: colorize with result
            }
        }
    }
}

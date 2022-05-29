using CommunityToolkit.Maui.Views;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.ColorizerPrompts;
using Maporizer.ColorizerPrompts.Models;
using Maporizer.Colorizers;

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
            var result = await page.ShowPopupAsync(popup);
            if (result is not PromptResultModel model)
            {
                return;
            }
            MessagingCenter.Send(this, "Colorize", model);
        }
    }
}

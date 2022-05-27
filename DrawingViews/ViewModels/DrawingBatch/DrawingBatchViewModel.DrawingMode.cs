using CommunityToolkit.Maui.Views;
using Maporizer.DrawingToolBarViews.Models;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.ColorizerPrompts;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

public partial class DrawingBatchViewModel
{
    public DrawingMode Mode { get; private set; }
    private void InitDrawingModeInternal()
    {
        Mode = DrawingMode.None;
        MessagingCenter.Subscribe<DrawingToolBarViewModel, ToolBarItemViewModel>(this, "ItemSelected", OnItemSelected);
    }
    private void OnItemSelected(DrawingToolBarViewModel _, ToolBarItemViewModel item)
    {
        Mode = item.Mode;
    }
}

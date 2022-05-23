using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews;

public partial class DrawingView : IDrawingView
{
    public DrawingMode Mode { get; private set; } = DrawingMode.None;
    private void ToolBarVM_InitInternal()
    {
        var toolbarVM = (DrawingToolBarViewModel)ToolBarView.BindingContext;
        toolbarVM.ItemSelected += ToolBarVM_ItemSelected;
    }
    private void ToolBarVM_ItemSelected(DrawingMode mode)
    {
        Mode = mode;
    }
}

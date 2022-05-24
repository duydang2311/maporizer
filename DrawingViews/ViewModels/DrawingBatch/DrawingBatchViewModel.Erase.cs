using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews.ViewModels.DrawingBatch;

using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class DrawingBatchViewModel
{
    private void InitEraseInternal()
    {
        GraphicsView.StartInteraction += Erase_View_StartInteraction;
    }
    private void Erase_View_StartInteraction(object? sender, TouchEventArgs e)
    {
        if (Root.Mode != DrawingMode.Erase)
        {
            return;
        }
        var drawable = (IDrawable)GraphicsView.Drawable;
        if (drawable.HoveringDrawing is not null)
        {
            drawable.Erase(drawable.HoveringDrawing);
            GraphicsView.Invalidate();
        }
    }
}

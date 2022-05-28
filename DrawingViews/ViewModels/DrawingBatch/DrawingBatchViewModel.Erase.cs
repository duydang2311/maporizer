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
        if (Mode != DrawingMode.Erase)
        {
            return;
        }
        var drawable = (IGraphicsDrawable)GraphicsView.Drawable;
        if (drawable.HoveringDrawing is not null)
        {
            drawable.Erase(drawable.HoveringDrawing);
            drawable.HoveringDrawing.Dispose();
            GraphicsView.Invalidate();
        }
    }
}

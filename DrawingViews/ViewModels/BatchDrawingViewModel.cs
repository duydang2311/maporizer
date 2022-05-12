using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
    private class BatchDrawingViewModel
    {
        private readonly DrawingView view;
        private IDrawable polygonBatch = null!;
        public BatchDrawingViewModel(DrawingView view)
        {
            this.view = view;
            view.GraphicsView.StartInteraction += GraphicsView_StartInteraction;
            view.GraphicsView.EndInteraction += GraphicsView_EndInteraction;
        }
        private void GraphicsView_StartInteraction(object? sender, TouchEventArgs e)
        {
            if (polygonBatch is null)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(50);
                });
            }
        }
        private void GraphicsView_EndInteraction(object? sender, TouchEventArgs e)
        {

        }
    }
}

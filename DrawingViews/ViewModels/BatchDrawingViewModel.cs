using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
    private class BatchDrawingViewModel
    {
        public DrawingView View { get; }
        public IDrawable PolygonBatch { get; private set; } = null!;
        public BatchDrawingViewModel(DrawingView view)
        {
            View = view;
            View.GraphicsView.StartInteraction += View_StartInteraction;
            View.GraphicsView.EndInteraction += View_EndInteraction;
        }
        private void View_StartInteraction(object? sender, TouchEventArgs e)
        {
            if (PolygonBatch is null)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(50);
                });
            }
        }
        private void View_EndInteraction(object? sender, TouchEventArgs e)
        {

        }
    }
}

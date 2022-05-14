using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.DrawingViews.ViewModels.DrawingBatch;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
	public DrawingView()
	{
		InitializeComponent();
        GraphicsView.Drawable = new GraphicsDrawableModel(GraphicsView);
        // Workaround for slider related issue, see https://github.com/dotnet/maui/issues/6957
        Task.Run(() =>
        {
            Thread.Sleep(0);
            App.Current!.Dispatcher.Dispatch(() =>
            {
                Slider.Value = 100;
            });
        });
        new DrawingBatchViewModel(GraphicsView);
	}
    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches[0];
        CoordinateLabel.Text = $"X: {(int)Math.Round(touch.X)}  Y: {(int)Math.Round(touch.Y)}";
    }
}
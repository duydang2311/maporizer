using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.DrawingViews.ViewModels.DrawingBatch;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews;

public partial class DrawingView
{
    // Yet another shitty workaround to implement IDrawingView
    // Because the actual graphics view property is not available until code-gen
    public IGraphicsView GraphicsView { get => _GraphicsView; }
	public DrawingView()
	{
		InitializeComponent();
        _GraphicsView.Drawable = new GraphicsDrawableModel(_GraphicsView);
        // Workaround for slider related issue, see https://github.com/dotnet/maui/issues/6957
        Task.Run(() =>
        {
            Thread.Sleep(0);
            App.Current!.Dispatcher.Dispatch(() =>
            {
                Slider.Value = 100;
            });
        });
        new DrawingBatchViewModel(_GraphicsView);
	}
    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches[0];
        CoordinateLabel.Text = $"X: {(int)Math.Round(touch.X)}  Y: {(int)Math.Round(touch.Y)}";
    }
}
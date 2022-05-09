using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
	public DrawingView()
	{
		InitializeComponent();
        GraphicsView.Drawable = new GraphicsDrawableModel(GraphicsView);
	}
    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches[0];
        CoordinateLabel.Text = $"X: {(int)Math.Round(touch.X)}  Y: {(int)Math.Round(touch.Y)}";
    }
}
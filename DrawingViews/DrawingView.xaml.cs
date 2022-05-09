using Maporizer.DrawingViews.ViewModels;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
	public DrawingView()
	{
		InitializeComponent();
        GraphicsView.Drawable = new GraphicsDrawableViewModel(GraphicsView);
	}
    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches[0];
        CoordinateLabel.Text = $"X: {(int)Math.Round(touch.X)}  Y: {(int)Math.Round(touch.Y)}";
    }
}
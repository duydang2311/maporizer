using Maporizer.DrawingViews.Models;

namespace Maporizer.DrawingViews;

public partial class DrawingView : ContentView
{
	private const int defaultSize = 100;
	public DrawingView()
	{
		InitializeComponent();
	}

    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
		var touch = e.Touches[0];
		CoordinateLabel.Text = $"X: {touch.X}  Y: {touch.Y}";
	}

    private void GraphicsView_StartInteraction(object sender, TouchEventArgs e)
    {
		(GraphicsView.Drawable as GraphicsDrawable).Draw(new EllipseModel
		{
			Location = e.Touches[0].Offset(-defaultSize / 2, -defaultSize / 2),
			Size = new Size(defaultSize),
			FillColor = App.Current.Resources["Primary"] as Color,
			StrokeColor = App.Current.Resources["Secondary"] as Color
        });
		GraphicsView.Invalidate();
    }
}
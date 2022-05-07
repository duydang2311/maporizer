namespace Maporizer.Views;

public partial class DrawingView : ContentView
{
	public DrawingView()
	{
		InitializeComponent();
	}

    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
		var touch = e.Touches[0];
		CoordinateLabel.Text = $"X: {touch.X}  Y: {touch.Y}";

	}
}
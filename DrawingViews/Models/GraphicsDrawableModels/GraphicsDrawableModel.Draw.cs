namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public partial class GraphicsDrawableModel : Microsoft.Maui.Graphics.IDrawable
{
    public void Draw(IDrawable drawing)
    {
        lock (Drawings)
        {
            Drawings.AddLast(drawing);
        }
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Antialias = true;
        lock (Drawings)
        {
            foreach (var drawing in Drawings)
            {
                drawing.Draw(canvas, dirtyRect);
            }
        }
    }
    private void GraphicsView_StartInteraction(object? sender, TouchEventArgs e)
    {
        var scaledOffset = -defaultSize * scaleFactor / 2;
        Draw(new EllipseModel
        {
            Location = e.Touches[0].Offset(scaledOffset, scaledOffset),
            Size = new Size(defaultSize * scaleFactor),
            FillColor = (Color)Application.Current!.Resources["Primary"],
            StrokeColor = (Color)Application.Current!.Resources["Secondary"]
        });
        GraphicsView.Invalidate();
    }
}


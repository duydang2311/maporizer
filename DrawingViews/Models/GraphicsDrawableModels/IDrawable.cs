namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public interface IDrawable : Microsoft.Maui.Graphics.IDrawable
{
    LinkedList<IDrawableShape> Drawings { get; }
    GraphicsView GraphicsView { get; }
    void Remove(IDrawableShape drawing);
    void Draw(IDrawableShape drawing);
}

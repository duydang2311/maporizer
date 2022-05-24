namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public interface IDrawable : Microsoft.Maui.Graphics.IDrawable
{
    LinkedList<IDrawableShape> Drawings { get; }
    GraphicsView GraphicsView { get; }
    void Erase(IDrawableShape drawing);
    void Draw(IDrawableShape drawing);
}

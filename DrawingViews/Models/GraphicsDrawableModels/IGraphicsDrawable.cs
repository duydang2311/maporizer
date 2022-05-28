namespace Maporizer.DrawingViews.Models.GraphicsDrawableModels;

public interface IGraphicsDrawable : IDrawable
{
    LinkedList<IDrawableShape> Drawings { get; }
    GraphicsView GraphicsView { get; }
    IDrawableShape? HoveringDrawing { get; }
    void Erase(IDrawableShape drawing);
    void Draw(IDrawableShape drawing);
    void PauseHovering();
    void ResumeHovering();
}

using Maporizer.MainPages;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews.ViewModels;

public class NewViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    public NewViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        MessagingCenter.Subscribe<MainPage>(view, "New", OnNew);
    }
    private void OnNew(MainPage sender)
    {
        drawable.Clear();
        view.GraphicsView.Invalidate();
    }
}

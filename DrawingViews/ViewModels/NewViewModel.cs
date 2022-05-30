using Maporizer.MainPages;
using Maporizer.Helpers;
using Maporizer.ColorizerPrompts.Models;
using Maporizer.Colorizers;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.DrawingToolBarViews.Models;

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
    }
}

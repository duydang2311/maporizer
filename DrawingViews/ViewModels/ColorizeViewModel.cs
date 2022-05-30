using Maporizer.MainPages;
using Maporizer.Helpers;
using Maporizer.ColorizerPrompts.Models;
using Maporizer.Colorizers;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews.ViewModels;

public class ColorizeViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    private IColorizer? colorizer;
    public ColorizeViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        MessagingCenter.Subscribe<MainPage, PromptResultModel>(view, "Colorize", OnColorize);
        MessagingCenter.Subscribe<DrawingToolBarViewModel, ToolBarItemViewModel>(this, "ItemSelected", OnItemSelected);
    }
    private void OnItemSelected(DrawingToolBarViewModel sender, ToolBarItemViewModel item)
    {
        if (colorizer is not null && item.Mode != DrawingMode.Colorize)
        {
            colorizer.Stop();
        }
    }
    private void OnColorize(MainPage sender, PromptResultModel model)
    {
        if (((IGraphicsDrawable)view.GraphicsView.Drawable).Drawings.Count == 0)
        {
            return;
        }
        if (model.Algorithm == "Brute-force")
        {
            Task.Run(() =>
            {
                colorizer = new BruteForceColorizer(OnIteration, model.Delay);
                var result = colorizer.Colorize(DrawableHelper.MakeAdjacencyMatrix(drawable));
            });
        }
    }
    private void OnIteration(int[] result)
    {
        var idx = 0;
        var colors = new Color[]
        {
            Colors.Green,
            Colors.Yellow,
            Colors.Red,
            Colors.Pink,
            Colors.Purple,
            Colors.Black,
            Colors.Brown,
            Colors.DarkCyan,
            Colors.Cyan,
            Colors.Tomato,
            Colors.Orange
        };
        view.Dispatcher.Dispatch(() =>
        {
            foreach (var i in drawable.Drawings)
            {
                i.FillColor = result[idx] == -1 ? Colors.Transparent : colors[result[idx]];
                ++idx;
            }
            view.GraphicsView.Invalidate();
        });
    }
}
